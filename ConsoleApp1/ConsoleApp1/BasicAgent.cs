using System.Linq;

namespace ConsoleApp1;

public class BasicAgent
{
    public Property GetFirstUnmortgaged(List<Property> pList)
    {
        int c = 0;
        Property prop = pList[c];
        while (prop.IsMortgaged && c + 1 < pList.Count)
        {
            c += 1;
            prop = pList[c];
        }
        if (prop.IsMortgaged)
        {
            return null;
        }
        else
        {
            return prop;
        }
    }

    // RAISE MONEY SOMEHOW -------------- $
    public void RaiseMoney(Player player)
    {
        double lImp = 100000;
        Property lImpProp = null;
        if (player.Owned.ContainsKey("u"))
        {
            lImpProp = GetFirstUnmortgaged(player.Owned["u"]);
            if (lImpProp != null)
            {
                lImp = lImpProp.Price;
                if (player.CheckMonopoly("u"))
                {
                    lImp = lImpProp.Price * 2.0;
                }
            }
        }
        if (player.Owned.ContainsKey("r"))
        {
            Property prop = GetFirstUnmortgaged(player.Owned["r"]);
            if (prop != null && lImp > prop.Price * prop.Multiplier)
            {
                lImp = prop.Price * prop.Multiplier;
                lImpProp = prop;
            }
        }
        for (int tier = 0; tier < 8; tier++)
        {
            if (player.Owned.ContainsKey(tier.ToString()))
            {
                Property cImpProp = GetFirstUnmortgaged(player.Owned[tier.ToString()]);
                if (cImpProp == null)
                {
                    continue;
                }
                double cImp = cImpProp.Price;
                if (player.CheckMonopoly(cImpProp.Color.ToString()))
                {
                    cImp = cImpProp.Price * player.Owned[cImpProp.Color.ToString()].Count + player.Owned[cImpProp.Color.ToString()].Sum(prop => prop.TotalPrice * prop.Building);
                }
                if (cImp < lImp)
                {
                    lImp = cImp;
                    lImpProp = cImpProp;
                }
            }
        }
        player.Mortgage(lImpProp);
    }

    // BUY OR NOT -------------- $
    public void BuyOrNot(Player player, Property property)
    {
        if (player.Money >= property.Price)
        {
            player.Buy(property, property.Price);
        }
    }

    // BUILD HOUSES OR NOT -------------- $
    public void BuildOrNot(Player player)
    {
        int monopolyTier = -1;
        for (int t = 7; t >= 0; t--)
        {
            if (player.CheckMonopoly(t.ToString()))
            {
                monopolyTier = t;
                break;
            }
        }
        if (monopolyTier == -1)
        {
            return;
        }
        List<Property> monopolyProperties = player.Owned[monopolyTier.ToString()];
        if (player.Money > 200.0)
        {
            double extraMoney = player.Money - 200.0;
            int numBuildings = (int)Math.Floor(extraMoney / monopolyProperties[0].TotalPrice);
            for (int building = 0; building < numBuildings; building++)
            {
                monopolyProperties[monopolyProperties.Count - building % monopolyProperties.Count - 1].MakeBuilding();
            }
        }
    }

}

