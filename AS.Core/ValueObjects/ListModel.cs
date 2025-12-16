namespace AS.Core.ValueObjects;

// <summary>
/// Detay iþlemlerinde kullanýlacak jenerik sýnýf
/// </summary>
public class ListModel<T> where T : class, new()
{

    public Paging Paging { get; set; }

    public List<T>? Items { get; set; }

}
