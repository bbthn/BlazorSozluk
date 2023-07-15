namespace BlazorSozluk.Clients.WebApp.Models;

public class FavClickedEventArgs : EventArgs
{
    public bool isFaved { get; set; }
    public Guid EntryId { get; set; }

}
