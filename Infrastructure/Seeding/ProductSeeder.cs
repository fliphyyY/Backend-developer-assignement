using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeding
{
    internal static class ProductSeeder
    {
        private static readonly Random random = new Random();
        private const int seedSize = 27;
        private static readonly string[] productNames = new string[seedSize] {
            "Intel Core i9 Processor",
            "AMD Ryzen 9 Processor",
            "NVIDIA GeForce RTX 3080",
            "AMD Radeon RX 6800",
            "Corsair Vengeance DDR4 RAM",
            "Samsung 970 EVO Plus SSD",
            "Western Digital Black HDD",
            "Asus ROG Strix Motherboard",
            "MSI MAG B550 TOMAHAWK",
            "Gigabyte Aorus Elite Motherboard",
            "NZXT Kraken X53 CPU Cooler",
            "Cooler Master Hyper 212",
            "Thermaltake Toughpower PSU",
            "Logitech G502 Gaming Mouse",
            "Razer DeathAdder V2 Mouse",
            "Corsair K95 RGB Keyboard",
            "SteelSeries Apex Pro Keyboard",
            "Dell Ultrasharp Monitor",
            "LG UltraGear Gaming Monitor",
            "HP LaserJet Printer",
            "Canon PIXMA Inkjet Printer",
            "Seagate Barracuda HDD",
            "Crucial MX500 SSD",
            "EVGA SuperNOVA PSU",
            "Kingston HyperX Fury RAM",
            "BenQ ZOWIE Gaming Monitor",
            "Alienware Aurora Gaming PC",
        };

        private static string[] imgUri = new string[seedSize]
        {
            "processors/intelCorei9Processor.jpg",
            "processors/amdRyzen9Processor.jpg",
            "graphicscards/nvidiaGeForceRtx3080.jpg",
            "graphicscards/amdRadeonRx6800.jpg",
            "memory/corsairVengeanceDdr4Ram.jpg",
            "storage/samsung970EvoPlusSsd.jpg",
            "storage/westernDigitalBlackHdd.jpg",
            "motherboards/asusRogStrixMotherboard.jpg",
            "motherboards/msiMagB550Tomahawk.jpg",
            "motherboards/gigabyteAorusEliteMotherboard.jpg",
            "cooling/nzxtKrakenX53CpuCooler.jpg",
            "cooling/coolerMasterHyper212.jpg",
            "power/thermaltakeToughpowerPsu.jpg",
            "peripherals/logitechG502GamingMouse.jpg",
            "peripherals/razerDeathAdderV2Mouse.jpg",
            "peripherals/corsairK95RgbKeyboard.jpg",
            "peripherals/steelSeriesApexProKeyboard.jpg",
            "monitors/dellUltrasharpMonitor.jpg",
            "monitors/lgUltraGearGamingMonitor.jpg",
            "printers/hpLaserJetPrinter.jpg",
            "printers/canonPixmaInkjetPrinter.jpg",
            "storage/seagateBarracudaHdd.jpg",
            "storage/crucialMx500Ssd.jpg",
            "power/evgaSuperNovaPsu.jpg",
            "memory/kingstonHyperXFuryRam.jpg",
            "monitors/benqZowieGamingMonitor.jpg",
            "computers/alienwareAuroraGamingPc.jpg"
        };

        private static string[] description = new string[seedSize]
        {
            "High-performance Intel Core i9 processor for demanding applications.",
            "AMD Ryzen 9 processor with impressive speed and energy efficiency.",
            "NVIDIA GeForce RTX 3080 graphics card for immersive gaming experiences.",
            "AMD Radeon RX 6800 with high frame rates and VR readiness.",
            null,
            "Samsung 970 EVO Plus SSD with high read/write speeds for quick data access.",
            null,
            "Corsair Vengeance DDR4 RAM for stable and fast performance.",
            "Logitech G502 gaming mouse with customizable weights and DPI settings.",
            "Razer DeathAdder V2 with ergonomic design for extended gaming sessions.",
            null,
            "Dell Ultrasharp monitor with vivid colors and 4K resolution.",
            "HP LaserJet printer, known for its speed and sharp print quality.",
            "Seagate Barracuda HDD offering large storage capacity for files and backups.",
            null,
            "Gigabyte Aorus Elite motherboard optimized for gaming setups.",
            "NZXT Kraken X53 CPU cooler with customizable RGB lighting.",
            "Cooler Master Hyper 212 for efficient air cooling and noise reduction.",
            null,
            "Corsair K95 RGB mechanical keyboard with customizable RGB backlighting.",
            "Asus ROG Strix motherboard with advanced cooling and connectivity features.",
            null,
            "Apple MacBook Pro, renowned for its sleek design and high performance.",
            "Microsoft Surface Laptop with a versatile touchscreen display.",
            null,
            "Canon EOS M50 camera, compact and perfect for high-quality photography.",
            "GoPro HERO9 Black, rugged and ready for action-packed adventures.",
        };
        internal static void ProductTableSeeder(ModelBuilder builder)
        {
            for (int i = 0; i < seedSize; i++)
            {
                builder.Entity<Product>().HasData(new Product()
                {
                    Id = i + 1,
                    Name = productNames[i],
                    ImgUri = imgUri[i],
                    Price = (float)(random.NextDouble() * (1000 - 50) + 50),
                    Description = description[i]
                });
            }
        }
    }
}
