namespace ConsoleApp1;
public class Railroad
{
    public double Price { get; private set; }
    public char Color { get; private set; }
    public double Rent { get; private set; }
    public int Multiplier { get; private set; }
    public double MortgageVal { get; private set; }
    public double UnmortgageVal { get; private set; }
    public bool IsMortgaged { get; private set; }
    public int Owner { get; private set; }

    public Railroad()
    {
        Price = 200.0;
        Color = 'r';
        Rent = 25.0;
        Multiplier = 1;
        MortgageVal = Price / 2;
        UnmortgageVal = MortgageVal * 1.1;
        IsMortgaged = false;
        Owner = -1;
    }

    public int get_owner()
    {
        return Owner;
    }

    public void set_owner(int new_owner)
    {
        Owner = new_owner;
    }

    public void on_another_bought()
    {
        Multiplier *= 2;
    }

    public void on_monopoly_lost()
    {
        Multiplier = 1;
    }

    public double get_rent(int moves)
    {
        if (IsMortgaged)
            return 0;
        return Multiplier * Rent;
    }

    public double mortgage()
    {
        if (!IsMortgaged)
        {
            IsMortgaged = true;
            return MortgageVal;
        }
        else
            return -1;
    }

    public double un_mortgage()
    {
        if (IsMortgaged)
        {
            IsMortgaged = false;
            return UnmortgageVal;
        }
        else
            return -1;
    }

    public void some_mortgaged()
    {
        // do nothing
    }

    public void some_unmortgaged()
    {
        // do nothing
    }
}