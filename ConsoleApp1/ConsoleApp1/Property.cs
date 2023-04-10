namespace ConsoleApp1;
public class Property
{
    public double Price;
    public int Color;
    public double TotalPrice;
    public double[] Rent;
    public double MortgageVal;
    public double UnMortgageVal;
    public int Building;
    public bool IsMortgaged;
    public int OwnerId;
    public bool Monopoly;
    public int NumMortgagedInSet;
    public int Multiplier;

    public Property(double price, int color, double[] rents)
    {
        this.Price = price;
        this.Color = color;
        this.TotalPrice = 50 * (color / 2 + 1);
        this.Rent = rents;
        this.MortgageVal = price / 2;
        this.UnMortgageVal = this.MortgageVal * 1.1;
        this.Building = 0;
        this.IsMortgaged = false;
        this.OwnerId = -1;
        this.Monopoly = false;
        this.NumMortgagedInSet = 0;
        this.Multiplier = 0;
    }

    public int GetOwner()
    {
        return this.OwnerId;
    }

    public void SetOwner(int newOwnerId)
    {
        this.OwnerId = newOwnerId;
    }

    public void OnMonopoly()
    {
        this.Monopoly = true;
    }

    public void OnMonopolyLost()
    {
        this.Monopoly = false;
    }

    public void SomeMortgaged()
    {
        this.NumMortgagedInSet += 1;
    }

    public void SomeUnmortgaged()
    {
        this.NumMortgagedInSet -= 1;
    }

    public double GetRent(int moves)
    {
        if (this.IsMortgaged)
        {
            return 0;
        }
        if (this.Building == 0 && this.Monopoly)
        {
            return this.Rent[0] * 2;
        }
        else
        {
            return this.Rent[this.Building];
        }
    }

    public double MakeBuilding()
    {
        if (this.Building < 5 && this.Monopoly && this.NumMortgagedInSet == 0)
        {
            this.Building += 1;
            return this.TotalPrice;
        }
        else
        {
            return -1;
        }
    }

    public double DestroyBuilding()
    {
        if (this.Building > 0)
        {
            this.Building -= 1;
            return this.TotalPrice / 2;
        }
        else
        {
            return -1;
        }
    }

    public double Mortgage()
    {
        if (!this.IsMortgaged)
        {
            this.IsMortgaged = true;
            return this.MortgageVal;
        }
        else
        {
            return -1;
        }
    }

    public double UnMortgage()
    {
        if (this.IsMortgaged)
        {
            this.IsMortgaged = false;
            return this.UnMortgageVal;
        }
        else
        {
            return -1;
        }
    }

    public void OnAnotherBought()
    {
        throw new NotImplementedException();
    }
}

