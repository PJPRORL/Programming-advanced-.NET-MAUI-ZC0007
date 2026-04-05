using System;

namespace MauiNavigationExample.Models;

public class Persoon
{
    public string Voornaam { get; set; }
    public DateTime Geboortedatum { get; set; } = DateTime.Now;
}
