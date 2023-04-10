namespace ConsoleApp1;

public class Player
{
    public int Id { get; set; }
    public Dictionary<string, List<Property>> Owned { get; private set; }
    public double Money { get; private set; }
    public double NetWorth { get; private set; }
    public int Location { get; private set; }

    public Player(double money)
    {
        Owned = new Dictionary<string, List<Property>>();
        this.Money = money;
        NetWorth = money;
        Location = -1;
    }

    public bool CheckMonopoly(string tier)
    {
        if (tier == "u" || tier == "0" || tier == "7")
        {
            if (!Owned.ContainsKey(tier))
            {
                return false;
            }

            return Owned[tier].Count == 2;
        }
        else
        {
            if (!Owned.ContainsKey(tier))
            {
                return false;
            }

            return Owned[tier].Count == 3;
        }
    }

    public int Move(int places)
    {
        Location += places;
        if (Location >= 40)
        {
            Money += 200.0;
        }

        Location %= 40;
        return Location;
    }

    public void Buy(Property property, double price)
    {
        Money -= price;
        property.SetOwner(this.Id);
        if (Owned.ContainsKey(property.Color.ToString()))
        {
            Owned[property.Color.ToString()].Add(property);
            Owned[property.Color.ToString()].Sort((x, y) => x.Price.CompareTo(y.Price));
            if (property.Color.ToString() == "r")
            {
                foreach (Property p in Owned["r"])
                {
                    p.OnAnotherBought();
                }
            }
            else if (CheckMonopoly(property.Color.ToString()))
            {
                foreach (Property p in Owned[property.Color.ToString()])
                {
                    p.OnMonopoly();
                }
            }
        }
        else
        {
            Owned[property.Color.ToString()] = new List<Property> { property };
        }

        NetWorth = NetWorth - price + (property.IsMortgaged ? 0 : property.MortgageVal);
    }

    public bool Mortgage(Property property)
    {
        double val = property.Mortgage();
        if (val != -1)
        {
            foreach (Property p in Owned[property.Color.ToString()])
            {
                p.SomeMortgaged();
            }

            Money += val;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Unmortgage(Property property)
    {
        double val = property.UnMortgage();
        if (val != -1)
        {
            foreach (Property p in Owned[property.Color.ToString()])
            {
                p.SomeUnmortgaged();
            }

            Money -= val;
            NetWorth = NetWorth - property.UnMortgageVal + property.MortgageVal;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Build(Property property)
    {
        double val = property.MakeBuilding();
        if (val != -1)
        {
            Money -= val;
            NetWorth = NetWorth - property.TotalPrice / 2;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Destroy(Property property)
    {
        double val = property.DestroyBuilding();
        if (val != -1)
        {
            Money += val;
            return true;
        }
        else
        {
            return false;
        }
    }

    public double PayRent(int rent)
    {
        if (Money >= rent)
        {
            Money -= rent;
            NetWorth -= rent;
            return rent;
        }
        else if (NetWorth >= rent)
        {
            return 0;
        }
        else
        {
            return Money;
        }
    }

    public void Defeated(Player other_player)
    {
        foreach (var v in Owned.Values)
        {
            foreach (var p in v)
            {
                other_player.Buy(p, 0);
            }
        }
    }

    public void DefeatedToBank()
    {
        foreach (var v in Owned.Values)
        {
            foreach (var p in v)
            {
                p.OwnerId = -1;
                p.OnMonopolyLost();
                p.UnMortgage();
                if (p is Property)
                {
                    ((Property)p).NumMortgagedInSet = 0;
                    ((Property)p).Building = 0;
                }
            }
        }
    }


}



