using System;
using System.Collections.Generic;

namespace cwiczenia7_ko_ubijak3.DAL;

public partial class Country
{
    public int IdCountry { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Trip> IdTrips { get; set; } = new List<Trip>();
}
