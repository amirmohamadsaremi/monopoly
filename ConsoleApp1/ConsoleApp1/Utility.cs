namespace ConsoleApp1;

public class Utility
{
    public double Price { get; private set; }
    public string Color { get; private set; }
    public double MortgageValue { get; private set; }
    public double UnmortgageValue { get; private set; }
    public bool IsMortgaged { get; private set; }
    public int Owner { get; private set; }
    public int Multiplier { get; private set; }

    public Utility()
    {
        Price = 150.0;
        Color = "u";
        MortgageValue = Price / 2;
        UnmortgageValue = MortgageValue * 1.1;
        IsMortgaged = false;
        Owner = -1;
        Multiplier = 4;
    }

    public int GetOwner()
    {
        return Owner;
    }

    public void SetOwner(int newOwner)
    {
        Owner = newOwner;
    }

    public void OnMonopoly()
    {
        Multiplier = 10;
    }

    public void OnMonopolyLost()
    {
        Multiplier = 4;
    }

    public double GetRent(int moves)
    {
        if (IsMortgaged)
        {
            return 0;
        }
        return Multiplier * moves;
    }

    public double Mortgage()
    {
        if (!IsMortgaged)
        {
            IsMortgaged = true;
            return MortgageValue;
        }
        else
        {
            return -1;
        }
    }

    public double Unmortgage()
    {
        if (IsMortgaged)
        {
            IsMortgaged = false;
            return UnmortgageValue;
        }
        else
        {
            return -1;
        }
    }
}

