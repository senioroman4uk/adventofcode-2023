using System.Text;

/// <summary>
/// 
/// </summary>
/// <param name="Id">Game id</param>
/// <param name="CubeCollection">A collection of cube subsets that elf showed to you</param>
internal record Game(int Id, IReadOnlyCollection<Bag> CubeCollection)
{
    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.Append($"Id = {Id}, ");
        builder.Append("CubeCollection = [");
        builder.Append(string.Join(", ", CubeCollection.Select(c => c.ToString())));
        builder.Append(']');
        return true;
    }
};