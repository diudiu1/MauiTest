using System.ComponentModel.DataAnnotations;

namespace MauiApi.Domain.Base
{
    public interface IEntityIdBase
    {
        [MaxLength(36)]
        string Id { get; set; }
    }
    public interface IEntityBase : IEntityIdBase
    {
        DateTime CreatTime { get; set; }
        DateTime UpdateTime { get; set; }
        bool IsDeleted { get; set; }
    }
    public class EntityBase : IEntityBase
    {
        /// <summary>
        /// ID,统一小写
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}