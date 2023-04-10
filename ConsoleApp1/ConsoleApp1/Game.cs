namespace ConsoleApp1;

class Game
{

    public Game(int num_players)
    {
        List<List<Property>> properties = new();
        List<Railroad> railroads = new();
        List<object> map = new();
        List<Player> players = new();
        Random random = new();

        // First brown set
        CreateProperties(properties, 0, 60, 60, 2, rents[0], rents[1]);
        for (int c = 1; c < 7; c++)
        {
            CreateProperties(
                properties,
                c,
                60 + c * 60,
                60 + c * 60,
                3,
                rents[c * 2],
                rents[c * 2 + 1]
            );
        }

        // Final Indigo set
        CreateProperties(properties, 7, 350, 400, 2, rents[14], rents[15]);

        // List of Railroads. All are identical.
        for (int i = 0; i < 4; i++)
        {
            railroads.Add(new Railroad());
        }

        // List of Utilities
        List<Utility> utilities = new() { new Utility(), new Utility() };

        // The Board. Stores indices of _properties etc.
        for (int i = 0; i < 40; i++)
        {
            map.Add(0);
        }

        // Brown set
        map[0] = properties[0][0];
        map[2] = properties[0][1];
        for (int i = 1; i < 7; i++)
        {
            map[i * 5] = properties[i][0];
            map[i * 5 + 2] = properties[i][1];
            map[i * 5 + 3] = properties[i][2];
        }

        // Indigo set
        map[36] = properties[7][0];
        map[38] = properties[7][1];

        // Railroads
        for (int i = 0; i < 4; i++)
        {
            map[i * 10 + 4] = railroads[i];
        }

        // Utilities
        map[11] = utilities[0];
        map[27] = utilities[1];

        map[3] = 200;
        map[37] = 100;

        for (int i = 0; i < num_players; i++)
        {
            players.Add(new Player(1500.0));
        }
    }

    private int[] rents = { 2, 10, 30, 90, 160, 250, 30, 90, 270, 400, 550, 750, 40, 100, 300, 450 };


    public void CreateProperties(List<List<Property>> listToAppend, int color, int lowPrice, int highPrice, int numProperties, int lowRents, int highRents)
    {
        List<Property> listProps = new List<Property>();
        for (int i = 1; i < numProperties; i++)
        {
            listProps.Add(new Property(lowPrice, color, new List<double>(){lowRents}.ToArray()));
        }
        listProps.Add(new Property(highPrice, color, new List<double>() { highRents }.ToArray()));
        listToAppend.Add(listProps);
    }

    public int RollDie()
    {
        Random random = new Random();
        return random.Next(1, 7) + random.Next(1, 7);
    }
}
