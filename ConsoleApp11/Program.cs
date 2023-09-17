using System;

abstract class Storage
{
    public string MediaType { get; set; }
    public string Model { get; set; }

    public abstract double GetCapacity();
    public abstract double Copy(double fileSize);
    public abstract double FreeMemory();
    public abstract void PrintDeviceInfo();
}

class Flash : Storage
{
    public double USB30Speed { get; set; }
    public double Memory { get; set; }

    public override double GetCapacity()
    {
        return Memory;
    }

    public override double Copy(double fileSize)
    {
        double requiredMedia = Math.Ceiling(fileSize / Memory);
        double timeRequired = requiredMedia * 780 / USB30Speed;
        return timeRequired;
    }

    public override double FreeMemory()
    {
        return 0; 
    }

    public override void PrintDeviceInfo()
    {
        Console.WriteLine($"Media Type: {MediaType}");
        Console.WriteLine($"Model: {Model}");
        Console.WriteLine($"USB 3.0 Speed: {USB30Speed} MB/s");
        Console.WriteLine($"Memory: {Memory} GB");
    }
}

class DVD : Storage
{
    public double ReadWriteSpeed { get; set; }
    public string Type { get; set; }

    public override double GetCapacity()
    {
        return (Type == "Single-Sided") ? 4.7 : 9.0;
    }

    public override double Copy(double fileSize)
    {
        double requiredMedia = Math.Ceiling(fileSize / GetCapacity());
        double timeRequired = requiredMedia * 1000 / ReadWriteSpeed; 
        return timeRequired;
    }

    public override double FreeMemory()
    {
        return 0;
    }

    public override void PrintDeviceInfo()
    {
        Console.WriteLine($"Media Type: {MediaType}");
        Console.WriteLine($"Model: {Model}");
        Console.WriteLine($"Read/Write Speed: {ReadWriteSpeed} MB/s");
        Console.WriteLine($"Type: {Type}");
    }
}

class HDD : Storage
{
    public double USB20Speed { get; set; }
    public double TotalSize { get; set; }

    public override double GetCapacity()
    {
        return TotalSize;
    }

    public override double Copy(double fileSize)
    {
        double requiredMedia = Math.Ceiling(fileSize / TotalSize);
        double timeRequired = requiredMedia * 780 / USB20Speed; 
        return timeRequired;
    }

    public override double FreeMemory()
    {
        return 0; 
    }

    public override void PrintDeviceInfo()
    {
        Console.WriteLine($"Media Type: {MediaType}");
        Console.WriteLine($"Model: {Model}");
        Console.WriteLine($"USB 2.0 Speed: {USB20Speed} MB/s");
        Console.WriteLine($"Total Size: {TotalSize} GB");
    }
}

class Program
{
    static void Main()
    {
        Storage flashDrive = new Flash
        {
            MediaType = "Flash Drive",
            Model = "SanDisk Ultra",
            USB30Speed = 150,
            Memory = 128
        };

        Storage dvdDisk = new DVD
        {
            MediaType = "DVD Disk",
            Model = "Verbatim DVD+R",
            ReadWriteSpeed = 8,
            Type = "Single-Sided"
        };

        Storage hddDrive = new HDD
        {
            MediaType = "HDD",
            Model = "Seagate Expansion",
            USB20Speed = 40, 
            TotalSize = 2000 
        };

        double fileSizeGB = 565;
        double flashCopyTime = flashDrive.Copy(fileSizeGB);
        double dvdCopyTime = dvdDisk.Copy(fileSizeGB);
        double hddCopyTime = hddDrive.Copy(fileSizeGB);

        flashDrive.PrintDeviceInfo();
        Console.WriteLine($"Time to copy {fileSizeGB} GB file: {flashCopyTime} seconds");

        dvdDisk.PrintDeviceInfo();
        Console.WriteLine($"Time to copy {fileSizeGB} GB file: {dvdCopyTime} seconds");

        hddDrive.PrintDeviceInfo();
        Console.WriteLine($"Time to copy {fileSizeGB} GB file: {hddCopyTime} seconds");
    }
}
