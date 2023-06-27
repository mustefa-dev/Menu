using AutoMapper;
using Menu.Data;
using Menu.Dtos.Section;
using Microsoft.EntityFrameworkCore;

namespace Menu.Services.Section
{
    public class SectionRepository : ISectionRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SectionRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SectionReadDto>> GetSections()
        {
            var sections = await _context.Sections.ToListAsync();
            return _mapper.Map<List<SectionReadDto>>(sections);
        }

        public async Task<SectionReadDto> GetSectionByName(string name)
        {
            var section = await _context.Sections.FirstOrDefaultAsync(s => s.Name == name);
            if (section == null)
            {
                return null;
            }

            return _mapper.Map<SectionReadDto>(section);
        }

        public async Task<(bool success, string message)> AddSection(Models.Section sectionCreateDto)
        {
            bool sectionExists = await _context.Sections.AnyAsync(s => s.Name == sectionCreateDto.Name);
            if (sectionExists)
            {
                return (false, "Section name already exists.");
            }

            var section = _mapper.Map<Models.Section>(sectionCreateDto);

            await _context.Sections.AddAsync(section);
            await _context.SaveChangesAsync();
            return (true, "Section added successfully.");
        }

        public async Task<(bool success, string message)> AddSection(SectionCreateDto sectionCreateDto,
            IWebHostEnvironment webHostEnvironment)
        {
            bool sectionExists = await _context.Sections.AnyAsync(s => s.Name == sectionCreateDto.Name);
            if (sectionExists)
            {
                return (false, "Section name already exists.");
            }

            var section = _mapper.Map<Models.Section>(sectionCreateDto);

            if (sectionCreateDto.Photo != null)
            {
                section.Photo = await SavePhoto(sectionCreateDto.Photo, webHostEnvironment);
            }

            await _context.Sections.AddAsync(section);
            await _context.SaveChangesAsync();

            return (true, "Section added successfully.");
        }

        public async Task<(bool success, string message)> UpdateSection(SectionUpdateDto sectionUpdateDto,
            IWebHostEnvironment webHostEnvironment)
        {
            var section = await _context.Sections.FirstOrDefaultAsync(s => s.Name == sectionUpdateDto.Name);
            if (section == null)
            {
                return (false, "Section not found.");
            }

            bool sectionExists = await _context.Sections.AnyAsync(s => s.Name == sectionUpdateDto.Name && s.Id != sectionUpdateDto.Id);
            if (sectionExists)
            {
                return (false, "Section name already exists.");
            }

            _mapper.Map(sectionUpdateDto, section);

            if (sectionUpdateDto.Photo != null)
            {
                section.Photo = await SavePhoto(sectionUpdateDto.Photo, webHostEnvironment);
            }

            await _context.SaveChangesAsync();
            return (true, "Section updated successfully.");
        }

        public async Task<(bool success, string message)> DeleteSection(string name)
        {
            var section = await _context.Sections.FirstOrDefaultAsync(s => s.Name == name);
            if (section == null)
            {
                return (false, "Section not found.");
            }

            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();
            return (true, "Section deleted successfully.");
        }


        private async Task<string> SavePhoto(IFormFile photo, IWebHostEnvironment webHostEnvironment) {
            var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder)) {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = $"{Guid.NewGuid()}_{photo.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Resize the photo to a 4:3 aspect ratio
            using (var image = SixLabors.ImageSharp.Image.Load(photo.OpenReadStream())) {
                int targetWidth = image.Width;
                int targetHeight = (int)(targetWidth * 0.75); // 4:3 aspect ratio

                image.Mutate(x => x.Resize(targetWidth, targetHeight));

                image.Save(filePath);
            }

            return Path.Combine("uploads", uniqueFileName);
        }
    }
}