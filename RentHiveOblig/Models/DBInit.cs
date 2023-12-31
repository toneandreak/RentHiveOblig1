﻿using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using RentHiveOblig.Controllers;
using RentHiveOblig.DAL;
using RentHiveOblig.Models;
using SQLitePCL;

namespace RentHiveOblig.Models
{
    public class DBInit
    {
        private readonly ApplicationDbContext _context;

        public DBInit(ApplicationDbContext context)
        {
            _context = context;
        }


        public static async Task Seed(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            ApplicationDbContext context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                var users = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        UserName = "Pål@oslomet.no",
                        Firstname = "Pål",
                        Lastname = "Mikkelson",
                        Email = "Pål@oslomet.no",
                    },
                    new ApplicationUser
                    {
                        UserName = "Andreas@oslomet.no",
                        Firstname = "Andreas",
                        Lastname = "Anderson",
                        Email = "Andreas@oslomet.no",
                    },
                    new ApplicationUser
                    {
                        UserName = "Linea@oslomet.no",
                        Firstname = "Linea",
                        Lastname = "Mørk",
                        Email = "Linea@oslomet.no",
                    },
                    new ApplicationUser
                    {
                        UserName = "Lars@oslomet.no",
                        Firstname = "Lars",
                        Lastname = "Petterson",
                        Email = "Lars@oslomet.no",
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Admin@123456");

                }

                context.AddRange(users);
                context.SaveChanges();
            }

            var createdUsers = context.ApplicationUsers.ToList();


            if (!context.Eiendom.Any())
            {
                var eiendoms = new List<Eiendom>
                {
                    new Eiendom
                    {
                        ApplicationUserId = createdUsers[0].Id,
                        PrisPerNatt = 1,
                        Tittel = "Cozy cabin, newly buildt near Oslo",
                        Beskrivelse = "Here lies a remarkable house of outstanding quality, " +
                        "meticulously built from teh ground up. It represents the very best in comfort, style, and craftsmanship. " +
                        "It consists of a large carport, 3 bedrooms, one bathroom, and a wardrobe in the hallway. A great family home.",
                        Street = "Ammerudgrenda",
                        City = "Oslo",
                        Country = "Norway",
                        ZipCode = "0958",
                        State = "Oslo",
                        Soverom = 3,
                        Bad = 1,
                        CreatedDateTime = DateTime.Now,
                        Image1 = "/images/house_1_s1",
                        Image2 = "/images/house_1_s2",
                        Image3 = "/images/house_1_s3",
                    },
                    new Eiendom
                    {
                        ApplicationUserId = createdUsers[0].Id,
                        PrisPerNatt = 1,
                        Tittel = "Cozy modern apartment",
                        Beskrivelse = "Welcome to this property, which is nicely located and very secluded at the end of a cul-de-sac. " +
                        "Close to the sea and Knarberg marina, which leads you directly into the idyllic Bjerkøysundet with many beautiful island opportunities in the archipelago. " +
                        "The house is also sunny and child-friendly. From the house, it's not far down to the sea. " +
                        "This area offers many great hiking opportunities and is close to the illuminated ski trail. ",
                        Street = "Tanbergveien",
                        City = "Gjøvik",
                        Country = "Norway",
                        ZipCode = "2819",
                        State = "Innlandet",
                        Soverom = 3,
                        Bad = 1,
                        CreatedDateTime = DateTime.Now,
                        Image1 = "/images/house_2_s1.jpg",
                        Image2 = "/images/house_2_s2.jpg",
                        Image3 = "/images/house_2_s3.jpg",
                    },
                    new Eiendom
                    {
                        ApplicationUserId = createdUsers[1].Id,
                        PrisPerNatt = 1,
                        Tittel = "Modern apartment near Tromsø",
                        Beskrivelse = "Here is a beautiful home that are to be built in the lovely Tromsdalen. " +
                        "It has a beautiful architectural design with large glass surfaces and exquisite material choices. " +
                        "Here you can live in peaceful surroundings while having all desired amenities within walking distance from the front door; " +
                        "school, kindergartens, recreational areas, illuminated ski trail, grocery store, as well as specialty stores with Pizza and Sushi. " +
                        "Good bus connections with a short distance to the bus stop.",
                        Street = "Alarmvegen",
                        City = "Lillehammer",
                        Country = "Norway",
                        ZipCode = "9020",
                        State = "Troms og Finnmark",
                        Soverom = 4,
                        Bad = 1,
                        CreatedDateTime = DateTime.Now,
                        Image1 = "/images/house_3_s1.jpg",
                        Image2 = "/images/house_3_s2.jpg",
                        Image3 = "/images/house_3_s3.jpg",
                    },
                    new Eiendom
                    {
                        ApplicationUserId = createdUsers[2].Id,
                        PrisPerNatt = 1,
                        Tittel = "Cozy large sized cabin",
                        Beskrivelse = "Detached turnkey single-family homes from Nordbohus Modum, with a lifetime standard, " +
                        "projected on plot 1, zoned for 5 single-family homes. Centrally located in Skurdalen. " +
                        "200 meters from the kindergarten, exit from Fv40, 10 km to Geilo, " +
                        "halfway between Oslo and Bergen by road and the Bergen Railway.",
                        Street = "Daglivegen",
                        City = "Geilo",
                        Country = "Norway",
                        ZipCode = "3580",
                        State = "Viken",
                        Soverom = 2,
                        Bad = 1,
                        CreatedDateTime = DateTime.Now,
                        Image1 = "/images/house_4_s1.jpg",
                        Image2 = "/images/house_4_s2.jpg",
                        Image3 = "/images/house_4_s3.jpg",
                    },
                    new Eiendom
                    {
                        ApplicationUserId = createdUsers[3].Id,
                        PrisPerNatt = 1,
                        Tittel = "Cozy small sized cabin",
                        Beskrivelse = "The mini-house have an area-efficient and rich floor plan. " +
                                      "The 1st floor comprises a hall with stairs, bathroom, storage room, technical room, one bedroom, " +
                                      "and a living room/kitchen in an open layout. The 2nd floor has a practical loft. " +
                                      "In connection with the entrance, a decking will be built with space for garden furniture. " +
                                      "The house comes with a sports storage room, as well as a parking space.",
                        Street = "Torvvegen",
                        City = "Reinsvoll",
                        Country = "Norway",
                        ZipCode = "2840",
                        State = "Innlandet",
                        Soverom = 1,
                        Bad = 1,
                        CreatedDateTime = DateTime.Now,
                        Image1 = "/images/house_5_s1.jpg",
                        Image2 = "/images/house_5_s2.jpg",
                        Image3 = "/images/house_5_s3.jpg",
                    },
                    new Eiendom
                    {
                        ApplicationUserId = createdUsers[4].Id,
                        PrisPerNatt = 1,
                        Tittel = "Medium sized modern cabin",
                        Beskrivelse = "Modern home where emphasis has been placed on quality. " +
                                      "Turnkey homes with no index regulation or additional costs. " +
                                      "Attractive area in Resahagen which is starting to take shape with schools, kindergarten, and sports field nearby. " +
                                      "3 low-maintenance detached houses with a sheltered outdoor area where one can enjoy the afternoon and evening sun directly from the kitchen/living room. " +
                                      "Practical home with 3 bedrooms, 2 living rooms, 2 bathrooms, laundry room, storage rooms. Carport with storage. " +
                                      "Upgraded kitchen with appliances from Sigdal. Separate kitchen island. Single plank laminate throughout the home on all floors except tiles in the bathrooms, " +
                                      "ensuring a consistent profile.",
                        Street = "Gaupevegen",
                        City = "Jørpeland",
                        Country = "Norway",
                        ZipCode = "4103",
                        State = "Rogaland",
                        Soverom = 3,
                        Bad = 1,
                        CreatedDateTime = DateTime.Now,
                        Image1 = "/images/house_6_s1.jpg",
                        Image2 = "/images/house_6_s2.jpg",
                        Image3 = "/images/house_6_s3.jpg",
                    },
                };
                context.AddRange(eiendoms);
                context.SaveChanges();
            }
        }
    }
}
