

namespace COSystem.EF.Repositories;

/// <summary> The Base Repository Class </summary>
/// <remarks> This class has the base intermediate methods (CRUD) between DbContext and User </remarks>
/// <typeparam name="T">The Model that will implement the base operations </typeparam>
public class BaseRepository<T> : IBaseService<T> where T : class
{
    private readonly AppDbContext _context;
    /// <summary>
    /// To Inject in Unit Of Work
    /// </summary>
    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }
    /// <summary>
    /// This method <c>Initialize</c> the entity
    /// </summary>
    /// <param name="entity">the object that will be initialized</param>
    /// <returns>A Task that represent adding to DbContext operation</returns>
    public async Task Create(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }
    /// <summary>
    /// This method <c>Delete</c> the entity
    /// </summary>
    /// <param name="entity">the object that will be deleted</param>
    /// <returns>A Task that represent deleteing from DbContext operation</returns>
    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
    /// <summary>
    /// This method <c>Update</c> the entity
    /// </summary>
    /// <param name="entity">the object that will be Updated</param>
    /// <returns>A Task that represent Update <typeparamref name="T"/> on DbContext operation</returns>
    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }
    /// <summary>
    ///     This method <c>Searching</c> in DbContext for the entity that fulfill <paramref name="match"/> boolean expression
    /// </summary>
    /// <param name="match">boolean expression that will searching depends on</param>
    /// <returns>A Task that represent searching in DbContext , Task result is object of type <typeparamref name="T"/> </returns>
    /// <exception cref="NullReferenceException">Thrown when DbContext does not have any entity that fulfill <paramref name="match"/>expression</exception>
    public async Task<T> FindAsync(Expression<Func<T, bool>> match)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(match);
    }
    /// <summary>
    ///   This method <c>Searching</c> in DbContext for the entity that fulfill <paramref name="match"/> boolean expression
    ///   and Implement Include() Method in Linq to retreive Navigation Properties
    /// </summary>
    /// <param name="match">boolean expression that will searching depends on</param>
    /// <param name="includes">Navigation Properties</param>
    /// <returns>A Task that represent searching in DbContext , Task result is object of type <typeparamref name="T"/> with it's Navigation Properties </returns>
    /// <exception cref="NullReferenceException">Thrown when DbContext does not have any entity that fulfill <paramref name="match"/>expression</exception>
    public async Task<T> FindAsync(Expression<Func<T, bool>> match, string[] includes = null!)
    {
        IQueryable<T> query = _context.Set<T>();
        if (includes != null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.FirstOrDefaultAsync(match);
    }
    /// <summary>
    ///   This method <c>Searching</c> in DbContext for the entities that fulfill <paramref name="match"/> boolean expression
    ///   and Implement Include() Method in Linq to retreive Navigation Properties
    /// </summary>
    /// <param name="match">boolean expression that will searching depends on</param>
    /// <param name="includes">Navigation Properties</param>
    /// <returns>A Task that represent searching in DbContext , Task result is ICollection of type <typeparamref name="T"/> with there Navigation Properties </returns>
    public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match, string[] includes = null!)
    {
        IQueryable<T> query = _context.Set<T>();
        if (includes != null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.Where(match).ToListAsync();
    }
    /// <summary>
    ///   This method <c>Retreive</c> from DbContext all the entities
    ///   and Implement Include() Method in Linq to retreive Navigation Properties
    /// </summary>
    /// <param name="includes">Navigation Properties</param>
    /// <returns>A Task that represent retreiving from DbContext , Task result is ICollection of type <typeparamref name="T"/> with there Navigation Properties </returns>
    public async Task<ICollection<T>> GetAllAsync(string[] includes = null!)
    {
        IQueryable<T> query = _context.Set<T>();
        if (includes != null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.ToListAsync();
    }
    /// <summary>
    ///   This method <c>Retreive</c> from DbContext all the entities
    /// </summary>
    /// <returns>A Task that represent retreiving from DbContext , Task result is ICollection of type <typeparamref name="T"/></returns>
    public async Task<ICollection<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }
    /// <summary>
    ///   This method <c>Retreive</c> from DbContext all the entities ordered depending on <paramref name="match"/> delegate
    /// </summary>
    /// <param name="match">a delegate that will ordering depends on </param>
    /// <returns>A Task that represent retreiving from DbContext , Task result is ICollection of type <typeparamref name="T"/> orderd depends on <paramref name="match"/> delegate </returns>
    public ICollection<T> GetAllOrdered(Func<T, string> match)
    {
        return _context.Set<T>().OrderBy(match).ToList();
    }

}
