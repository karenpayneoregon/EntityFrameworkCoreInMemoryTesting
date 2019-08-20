using Microsoft.EntityFrameworkCore;

namespace TestProject.Classes
{
    public static class EntityExtensions
    {
        /// <summary>
        /// Used to flush mocked table, not recommended for production
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbSet"></param>
        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }
    }
}
