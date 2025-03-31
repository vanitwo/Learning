class Program
{
    static void Main(string[] args)
    {
        var tanks = GetTanks();
        var units = GetUnits();
        var factories = GetFactories();
        Console.WriteLine($"Количество резервуаров: {tanks.Length}, установок: {units.Length}");

        var foundUnit = FindUnit(units, tanks, "Резервуар 2");
        var factory = FindFactory(factories, foundUnit);

        Console.WriteLine($"Резервуар 2 принадлежит установке {foundUnit.Name} и заводу {factory.Name}");

        var totalVolume = GetTotalVolume(tanks);
        Console.WriteLine($"Общий объем резервуаров: {totalVolume}");
    }
    
    public static Tank[] GetTanks()
    {
        var tanks = new Tank[]
        {
            new Tank(1, "Резервуар 1", "Надземный - вертикальный", 1500, 2000, GetUnits()[0]),
            new Tank(1, "Резервуар 2", "Надземный - горизонтальный", 2500, 3000, GetUnits()[0]),
            new Tank(1, "Дополнительный резервуар 24", "Надземный - горизонтальный", 3000, 3000, GetUnits()[1]),
            new Tank(1, "Резервуар 35", "Надземный - вертикальный", 3000, 3000, GetUnits()[1]),
            new Tank(1, "Резервуар 47", "Подземный - двустенный", 4000, 5000, GetUnits()[1]),
            new Tank(1, "Резервуар 256", "Подводный", 500, 500, GetUnits()[2])
        };
        return tanks;
    }
    
    public static Unit[] GetUnits()
    {
        var units = new Unit[]
        {
            new Unit(1, "ГФУ-2", "Газофракционирующая установка", GetFactories()[0]),
            new Unit(1, "АВТ-6", "Атмосферно-вакуумная трубчатка", GetFactories()[0]),
            new Unit(1, "АВТ-10", "Атмосферно-вакуумная трубчатка", GetFactories()[1])
        };
        return units;
    }
    
    public static Factory[] GetFactories()
    {
        var factories = new Factory[]
        {
            new Factory(1, "НПЗ№1", "Первый нефтеперерабатывающий завод"),
            new Factory(2, "НПЗ№2", "Второй нефтеперерабатывающий завод")
        };
        return factories;
    }
    
    public static Unit FindUnit(Unit[] units, Tank[] tanks, string unitName)
    {
        var tank = tanks.FirstOrDefault(t => t.Name == unitName);
        if (tank == null)
            return null;
        return units.FirstOrDefault(u => u.Id == tank.UnitId);
    }
    
    public static Factory FindFactory(Factory[] factories, Unit unit)
    {
        return factories.FirstOrDefault(f => f.Id == unit.FactoryId);
    }
    
    public static int GetTotalVolume(Tank[] units)
    {
        return units.Sum(u => u.Volume);
    }
}

/// <summary>
/// Установка
/// </summary>
public class Unit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int FactoryId { get; set; }    

    public Unit(int id, string name, string description, Factory factory)
    {
        Id = id;
        Name = name;
        Description = description;
        FactoryId = factory.Id;
    }
}

/// <summary>
/// Завод
/// </summary>
public class Factory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    

    public Factory(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
}

/// <summary>
/// Резервуар
/// </summary>
public class Tank
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int UnitId { get; set; }
    public int Volume {  get; set; }
    public int MaxVolume {  get; set; }
    

    public Tank(int id, string name, string description, int volume, int maxVolume, Unit unit)
    {
        Id = id;
        Name = name;
        Description = description;
        Volume = volume;
        MaxVolume = maxVolume;
        UnitId = unit.Id;
    }
}