using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class HotelCapacity
{
    static bool CheckCapacity(int maxCapacity, List<Guest> guests)
    {
        var dates = new List<(string, int)>();

        foreach (var guest in guests)
        {
            dates.Add((guest.CheckIn, 1));
            dates.Add((guest.CheckOut, -1));
        }

        var sortedDates = dates
            .OrderBy(item => item.Item1)
            .ThenBy(item => item.Item2);

        var currentCapacity = 0;

        foreach (var date in sortedDates)
        {
            currentCapacity += date.Item2;

            if (currentCapacity > maxCapacity)
                return false;
        }
        
        return true;
    }
    
    class Guest
    {
        public string Name { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
    }
    
    static void Main()
    {
        int maxCapacity = int.Parse(Console.ReadLine());
        int n = int.Parse(Console.ReadLine());
        
        List<Guest> guests = new List<Guest>();

        for (int i = 0; i < n; i++)
        {
            string line = Console.ReadLine();
            Guest guest = ParseGuest(line);
            guests.Add(guest);
        }
        
        bool result = CheckCapacity(maxCapacity, guests);
        
        Console.WriteLine(result ? "True" : "False");
    }
    
    static Guest ParseGuest(string json)
    {
        var guest = new Guest();
        
        Match nameMatch = Regex.Match(json, "\"name\"\\s*:\\s*\"([^\"]+)\"");
        if (nameMatch.Success)
            guest.Name = nameMatch.Groups[1].Value;
        
        Match checkInMatch = Regex.Match(json, "\"check-in\"\\s*:\\s*\"([^\"]+)\"");
        if (checkInMatch.Success)
            guest.CheckIn = checkInMatch.Groups[1].Value;
        
        Match checkOutMatch = Regex.Match(json, "\"check-out\"\\s*:\\s*\"([^\"]+)\"");
        if (checkOutMatch.Success)
            guest.CheckOut = checkOutMatch.Groups[1].Value;
        
        return guest;
    }
}