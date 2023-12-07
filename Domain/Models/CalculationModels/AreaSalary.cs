using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.CalculationModels
{
    public class AreaPopularSalary : BaseModel
    {
        public long AreaId { get; set; }
        public int Price { get; set; }

        public long PersonQuntity { get; set; }


        public static AreaPopularSalary[] GetAreaPopularSalaries()
        {
            return new AreaPopularSalary[]
            {
                //Almaty
                   new AreaPopularSalary
                   {
                       AreaId = 1, PersonQuntity = 0, Price = 5375
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 1, PersonQuntity = 1, Price = 96750
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 1, PersonQuntity = 2, Price = 193500
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 1, PersonQuntity = 3, Price = 290250
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 1, PersonQuntity = 4, Price = 387000
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 1, PersonQuntity = 5, Price = 489750
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 1, PersonQuntity = 6, Price = 580500
                   },
                     new AreaPopularSalary
                   {
                       AreaId = 1, PersonQuntity = 7, Price = 677250
                   },
                     //Astana
                     new AreaPopularSalary
                   {
                       AreaId = 2, PersonQuntity = 0, Price = 4614
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 2, PersonQuntity = 1, Price = 83052
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 2, PersonQuntity = 2, Price = 166104
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 2, PersonQuntity = 3, Price = 249156
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 2, PersonQuntity = 4, Price = 332208
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 2, PersonQuntity = 5, Price = 415260
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 2, PersonQuntity = 6, Price = 498312
                   },
                     new AreaPopularSalary
                   {
                       AreaId = 2, PersonQuntity = 7, Price = 581364
                   },
                     //КОНАЕВ
                       new AreaPopularSalary
                   {
                       AreaId = 3, PersonQuntity = 0, Price = 3650
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 3, PersonQuntity = 1, Price = 65700
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 3, PersonQuntity = 2, Price = 131400
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 3, PersonQuntity = 3, Price = 197100
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 3, PersonQuntity = 4, Price = 262800
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 3, PersonQuntity = 5, Price = 328500
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 3, PersonQuntity = 6, Price = 394200
                   },
                     new AreaPopularSalary
                   {
                       AreaId = 3, PersonQuntity = 7, Price = 459900
                   },
                       //Актау
                       new AreaPopularSalary
                   {
                       AreaId = 4, PersonQuntity = 0, Price = 3244
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 4, PersonQuntity = 1, Price = 58392
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 4, PersonQuntity = 2, Price = 116784
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 4, PersonQuntity = 3, Price = 175176
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 4, PersonQuntity = 4, Price = 233568
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 4, PersonQuntity = 5, Price = 291960
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 4, PersonQuntity = 6, Price = 350352
                   },
                     new AreaPopularSalary
                   {
                       AreaId = 4, PersonQuntity = 7, Price = 408744
                   },
                     //Караганда
                       new AreaPopularSalary
                   {
                       AreaId = 5, PersonQuntity = 0, Price = 3082
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 5, PersonQuntity = 1, Price = 55476
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 5, PersonQuntity = 2, Price = 110952
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 5, PersonQuntity = 3, Price = 166428
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 5, PersonQuntity = 4, Price = 221904
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 5, PersonQuntity = 5, Price = 277380
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 5, PersonQuntity = 6, Price = 332856
                   },
                     new AreaPopularSalary
                   {
                       AreaId = 5, PersonQuntity = 7, Price = 388332
                   },
                       //Жезказган
                       new AreaPopularSalary
                   {
                       AreaId = 6, PersonQuntity = 0, Price = 2797
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 6, PersonQuntity = 1, Price = 50346
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 6, PersonQuntity = 2, Price = 100692
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 6, PersonQuntity = 3, Price = 151038
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 6, PersonQuntity = 4, Price = 201384
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 6, PersonQuntity = 5, Price = 251730
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 6, PersonQuntity = 6, Price = 302076
                   },
                     new AreaPopularSalary
                   {
                       AreaId = 6, PersonQuntity = 7, Price = 352422
                   },
                         //Шымкент
                       new AreaPopularSalary
                   {
                       AreaId = 7, PersonQuntity = 0, Price = 2731
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 7, PersonQuntity = 1, Price = 49158
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 7, PersonQuntity = 2, Price = 98316
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 7, PersonQuntity = 3, Price = 147474
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 7, PersonQuntity = 4, Price = 196632
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 7, PersonQuntity = 5, Price = 245790
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 7, PersonQuntity = 6, Price = 294948
                   },
                     new AreaPopularSalary
                   {
                       AreaId = 7, PersonQuntity = 7, Price = 344106
                   },
                            //Атырау
                       new AreaPopularSalary
                   {
                       AreaId = 8, PersonQuntity = 0, Price = 2524
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 8, PersonQuntity = 1, Price = 45432
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 8, PersonQuntity = 2, Price = 90864
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 8, PersonQuntity = 3, Price = 136296
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 8, PersonQuntity = 4, Price = 181728
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 8, PersonQuntity = 5, Price = 227160
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 8, PersonQuntity = 6, Price = 272592
                   },
                     new AreaPopularSalary
                   {
                       AreaId = 8, PersonQuntity = 7, Price = 318024
                   },
                             //Актобе
                       new AreaPopularSalary
                   {
                       AreaId = 9, PersonQuntity = 0, Price = 2472
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 9, PersonQuntity = 1, Price = 44496
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 9, PersonQuntity = 2, Price = 88992
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 9, PersonQuntity = 3, Price = 133488
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 9, PersonQuntity = 4, Price = 177984
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 9, PersonQuntity = 5, Price = 222480
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 9, PersonQuntity = 6, Price = 266976
                   },
                     new AreaPopularSalary
                   {
                       AreaId = 9, PersonQuntity = 7, Price = 311472
                   },
                          //Костанай
                       new AreaPopularSalary
                   {
                       AreaId = 10, PersonQuntity = 0, Price = 2466
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 10, PersonQuntity = 1, Price = 44388
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 10, PersonQuntity = 2, Price = 88776
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 10, PersonQuntity = 3, Price = 133164
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 10, PersonQuntity = 4, Price = 177552
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 10, PersonQuntity = 5, Price = 221940
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 10, PersonQuntity = 6, Price = 266328
                   },
                     new AreaPopularSalary
                   {
                       AreaId = 10, PersonQuntity = 7, Price = 310716
                   },
                     //Павлодар
                    new AreaPopularSalary
                   {
                       AreaId = 11, PersonQuntity = 0, Price = 2464
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 11, PersonQuntity = 1, Price = 44352
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 11, PersonQuntity = 2, Price = 88704
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 11, PersonQuntity = 3, Price = 133056
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 11, PersonQuntity = 4, Price = 177408
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 11, PersonQuntity = 5, Price = 221760
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 11, PersonQuntity = 6, Price = 266112
                   },
                     new AreaPopularSalary
                   {
                       AreaId = 11, PersonQuntity = 7, Price = 310464
                   },
                      //Кокшетау
                    new AreaPopularSalary
                   {
                       AreaId = 12, PersonQuntity = 0, Price = 2439
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 12, PersonQuntity = 1, Price = 43902
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 12, PersonQuntity = 2, Price = 87804
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 12, PersonQuntity = 3, Price = 131706
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 12, PersonQuntity = 4, Price = 175608
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 12, PersonQuntity = 5, Price = 219510
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 12, PersonQuntity = 6, Price = 263412
                   },
                     new AreaPopularSalary
                   {
                       AreaId = 12, PersonQuntity = 7, Price = 307314
                   },
                       //Оскемен
                    new AreaPopularSalary
                   {
                       AreaId = 13, PersonQuntity = 0, Price = 2353
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 13, PersonQuntity = 1, Price = 42354
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 13, PersonQuntity = 2, Price = 84708
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 13, PersonQuntity = 3, Price = 127032
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 13, PersonQuntity = 4, Price = 169416
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 13, PersonQuntity = 5, Price = 211770
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 13, PersonQuntity = 6, Price = 254124
                   },
                     new AreaPopularSalary
                   {
                       AreaId = 13, PersonQuntity = 7, Price = 296478
                   },
                        //Уральск
                    new AreaPopularSalary
                   {
                       AreaId = 14, PersonQuntity = 0, Price = 2387
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 14, PersonQuntity = 1, Price = 42966
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 14, PersonQuntity = 2, Price = 85932
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 14, PersonQuntity = 3, Price = 128898
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 14, PersonQuntity = 4, Price = 171864
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 14, PersonQuntity = 5, Price = 214830
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 14, PersonQuntity = 6, Price = 257796
                   },
                     new AreaPopularSalary
                   {
                       AreaId = 14, PersonQuntity = 7, Price = 300762
                   },
                          //Петропавл
                    new AreaPopularSalary
                   {
                       AreaId = 15, PersonQuntity = 0, Price = 2338
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 15, PersonQuntity = 1, Price = 42044
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 15, PersonQuntity = 2, Price = 84168
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 15, PersonQuntity = 3, Price = 126252
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 15, PersonQuntity = 4, Price = 168336
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 15, PersonQuntity = 5, Price = 210420
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 15, PersonQuntity = 6, Price = 252504
                   },
                     new AreaPopularSalary
                   {
                       AreaId = 15, PersonQuntity = 7, Price = 294588
                   },
                           //Туркестан
                    new AreaPopularSalary
                   {
                       AreaId = 16, PersonQuntity = 0, Price = 2265
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 16, PersonQuntity = 1, Price = 40770
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 16, PersonQuntity = 2, Price = 81540
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 16, PersonQuntity = 3, Price = 122310
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 16, PersonQuntity = 4, Price = 163080
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 16, PersonQuntity = 5, Price = 203850
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 16, PersonQuntity = 6, Price = 244620
                   },
                     new AreaPopularSalary
                   {
                       AreaId = 16, PersonQuntity = 7, Price = 285390
                   },
                    //Талдыкорган
                    new AreaPopularSalary
                   {
                       AreaId = 17, PersonQuntity = 0, Price = 2260
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 17, PersonQuntity = 1, Price = 40680
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 17, PersonQuntity = 2, Price = 81360
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 17, PersonQuntity = 3, Price = 122040
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 17, PersonQuntity = 4, Price = 162720
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 17, PersonQuntity = 5, Price = 203400
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 17, PersonQuntity = 6, Price = 244080
                   },
                     new AreaPopularSalary
                   {
                       AreaId = 17, PersonQuntity = 7, Price = 284760
                   },
                      //Семей
                    new AreaPopularSalary
                   {
                       AreaId = 18, PersonQuntity = 0, Price = 2188
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 18, PersonQuntity = 1, Price = 39384
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 18, PersonQuntity = 2, Price = 78768
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 18, PersonQuntity = 3, Price = 118152
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 18, PersonQuntity = 4, Price = 157536
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 18, PersonQuntity = 5, Price = 196920
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 18, PersonQuntity = 6, Price = 236304
                   },
                     new AreaPopularSalary
                   {
                       AreaId = 18, PersonQuntity = 7, Price = 275688
                   },
                         //Кызылорда
                    new AreaPopularSalary
                   {
                       AreaId = 19, PersonQuntity = 0, Price = 1943
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 19, PersonQuntity = 1, Price = 34974
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 19, PersonQuntity = 2, Price = 69948
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 19, PersonQuntity = 3, Price = 104922
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 19, PersonQuntity = 4, Price = 139896
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 19, PersonQuntity = 5, Price = 174870
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 19, PersonQuntity = 6, Price = 209844
                   },
                     new AreaPopularSalary
                   {
                       AreaId = 19, PersonQuntity = 7, Price = 244818
                   },
                          //Тараз
                    new AreaPopularSalary
                   {
                       AreaId = 20, PersonQuntity = 0, Price = 1853
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 20, PersonQuntity = 1, Price = 33354
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 20, PersonQuntity = 2, Price = 66708
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 20, PersonQuntity = 3, Price = 100062
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 20, PersonQuntity = 4, Price = 133416
                   },
                   new AreaPopularSalary
                   {
                       AreaId = 20, PersonQuntity = 5, Price = 166770
                   },
                    new AreaPopularSalary
                   {
                       AreaId = 20, PersonQuntity = 6, Price = 200124
                   },
                     new AreaPopularSalary
                   {
                       AreaId = 20, PersonQuntity = 7, Price = 233478
                   },
            };

        }


    }


    public class AreaSalary : BaseModel
    {
        public string TitleRu { get; set; }
        public string TitleEn { get; set; }
        public string TitleKz { get; set; }


        public static AreaSalary[] GetAllSalariesArea()
        {
            return new AreaSalary[]
            {
                new AreaSalary
                {
                    Id = 1,
                    TitleRu = "Алматы",
                    TitleKz = "Алматы",
                    TitleEn = "Алматы",
                },
                new AreaSalary
                {
                    Id = 2,
                    TitleRu = "Астана",
                    TitleKz = "Астана",
                    TitleEn = "Астана",
                },
                 new AreaSalary
                {
                    Id = 3,
                    TitleRu = "КОНАЕВ",
                    TitleKz = "КОНАЕВ",
                    TitleEn = "КОНАЕВ",
                },
                  new AreaSalary
                {
                    Id = 4,
                    TitleRu = "Актау",
                    TitleKz = "Актау",
                    TitleEn = "Актау",
                },
                new AreaSalary
                {
                    Id = 5,
                    TitleRu = "Караганда",
                    TitleKz = "Караганда",
                    TitleEn = "Караганда",
                },
                new AreaSalary
                {
                    Id = 6,
                    TitleRu = "Жезказган",
                    TitleKz = "Жезказган",
                    TitleEn = "Жезказган",
                },
                 new AreaSalary
                {
                    Id = 7,
                    TitleRu = "Шымкент",
                    TitleKz = "Шымкент",
                    TitleEn = "Шымкент",
                },
                   new AreaSalary
                {
                    Id = 8,
                    TitleRu = "Атырау",
                    TitleKz = "Атырау",
                    TitleEn = "Атырау",
                },
                new AreaSalary
                {
                    Id = 9,
                    TitleRu = "Актобе",
                    TitleKz = "Актобе",
                    TitleEn = "Актобе",
                },
                new AreaSalary
                {
                    Id = 10,
                    TitleRu = "Костанай",
                    TitleKz = "Костанай",
                    TitleEn = "Костанай",
                },
                new AreaSalary
                {
                    Id = 11,
                    TitleRu = "Павлодар",
                    TitleKz = "Павлодар",
                    TitleEn = "Павлодар",
                },
                new AreaSalary
                {
                    Id = 12,
                    TitleRu = "Кокшетау",
                    TitleKz = "Кокшетау",
                    TitleEn = "Кокшетау",
                },
                 new AreaSalary
                {
                    Id = 13,
                    TitleRu = "Оскемен",
                    TitleKz = "Оскемен",
                    TitleEn = "Оскемен",
                },
                  new AreaSalary
                {
                    Id = 14,
                    TitleRu = "Уральск",
                    TitleKz = "Уральск",
                    TitleEn = "Уральск",
                },
                    new AreaSalary
                {
                    Id = 15,
                    TitleRu = "Петропавл",
                    TitleKz = "Петропавл",
                    TitleEn = "Петропавл",
                },
                 new AreaSalary
                {
                    Id = 16,
                    TitleRu = "Туркестан",
                    TitleKz = "Туркестан",
                    TitleEn = "Туркестан",
                },
                 new AreaSalary
                {
                    Id = 17,
                    TitleRu = "Талдыкорган",
                    TitleKz = "Талдыкорган",
                    TitleEn = "Талдыкорган",
                },
                new AreaSalary
                {
                    Id = 18,
                    TitleRu = "Семей",
                    TitleKz = "Семей",
                    TitleEn = "Семей",
                },
                new AreaSalary
                {
                    Id = 19,
                    TitleRu = "Кызылорда",
                    TitleKz = "Кызылорда",
                    TitleEn = "Кызылорда",
                },
                new AreaSalary
                {
                    Id = 20,
                    TitleRu = "Тараз",
                    TitleKz = "Тараз",
                    TitleEn = "Тараз",
                },
            };
        }

    }


    
}
