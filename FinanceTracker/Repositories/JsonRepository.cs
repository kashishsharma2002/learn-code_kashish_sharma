using System.Text.Json;
using FinanceTracker.Api.Common;

namespace FinanceTracker.Api.Repositories
{
    public class JsonRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly string _filePath;

        public JsonRepository(string fileName)
        {
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            _filePath = Path.Combine(folder, fileName);
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        private async Task<List<T>> ReadAllAsync()
        {
            using var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        private async Task WriteAllAsync(List<T> data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            using var stream = new FileStream(_filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            using var writer = new StreamWriter(stream);
            await writer.WriteAsync(json);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await ReadAllAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            var all = await ReadAllAsync();
            return all.FirstOrDefault(e => GetId(e) == id);
        }

        public async Task AddAsync(T entity)
        {
            var all = await ReadAllAsync();
            all.Add(entity);
            await WriteAllAsync(all);
        }

        public async Task UpdateAsync(T entity)
        {
            var all = await ReadAllAsync();
            var index = all.FindIndex(e => GetId(e) == GetId(entity));
            if (index != -1)
            {
                all[index] = entity;
                await WriteAllAsync(all);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var all = await ReadAllAsync();
            var item = all.FirstOrDefault(e => GetId(e) == id);
            if (item != null)
            {
                all.Remove(item);
                await WriteAllAsync(all);
            }
        }

        private Guid GetId(T entity) => entity.Id;
    }
}
