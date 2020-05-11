using PatcientInfo.Data.Interfeces;
using PatcientInfo.Entities;
using System.Collections.Generic;
using System.Linq;


namespace PatcientInfo.Data
{
   public static class TestDataCreation
    {
        public static void CreateDicaseType(ICollection<DicaseType> collection)
        {
            collection.Add(new DicaseType("Хвороба", null)
            {
                Id = 1,
                Descripotion= "Хвороба (або захворювання) — патологічний процес, який проявляється порушеннями морфології (анатомічної, гістологічної будови), обміну речовин чи / та функціонування організму (його частин) у людини / тварини.",
                Nazva= "Хвороба",
            });
            collection.Add(new DicaseType("Інфекційні",
               collection.First(e => e.Nazva == "Хвороба"))
            {
                Id = 2,
                Nazva="Інфекційні",
                Descripotion= "Інфекці́йні захво́рювання / хворо́би — розлади здоров'я людей, тварин, рослин у вигляді хвороби, які спричинюють збудники — віруси, різноманітні бактерії, найпростіші, паразитичні гриби, гельмінти, продукти їх життєдіяльності (екзотоксини, ендотоксини), патогенні білки — пріони, здатні передаватися від заражених організмів здоровим і схильні до масового поширення.",
                
            });
            collection.Add(new DicaseType("Нервової сиситеми",
              collection.First(e => e.Nazva == "Хвороба"))
            {
                Id = 3,
                Nazva = "Нервової сиситеми",
                Descripotion="Це захворюввання які повязані з розладами нервової системи"
            });
            collection.Add(new DicaseType("Кровоносної системи",
             collection.First(e => e.Nazva == "Хвороба"))
            {
                Id = 4,
                Nazva = "Кровоносної системи",
                Descripotion = "Кровоо́біг — процес постійної циркуляції крові в організмі, що забезпечує його життєдіяльність. Кровоносну систему організму іноді об'єднують із лімфатичною системою в серцево-судинну систему.",
            });

        }
        public static void CreateDicases(IDateSet dataSet)
        {
            dataSet.Discases.Add(new Discase("Вітрянка",
                dataSet.DicaseTypes
                    .First(e => e.Nazva == "Інфекційні"))
            {
                
                Id = 4,
                Nazva="Вітрянка",
                Descripotion= "Вітряна́ ві́спа (просторічне — вітря́нка, англ. chikenpox[1], лат. varicella[2], грец. Ανεμοβλογιά) — контагіозне вірусне захворювання з групи герпесвірусних інфекцій, яке характеризується переважним ураженням дітей, помірною загальною інтоксикацією, поліморфною екзантемою з переважанням везикул.",


            });
            dataSet.Discases.Add(new Discase("Кір",
              dataSet.DicaseTypes
                  .First(e => e.Nazva == "Інфекційні"))
            {

                Id = 5,
                Nazva = "Кір",
                Descripotion= "Кір (лат. morbilli; англ. measles / rubeola) — антропонозна інфекційна хвороба, яку спричинює вірус з роду Morbillivirus. Характеризується вираженою автоінтоксикацією, гарячкою, запальними явищами з боку дихальних шляхів, кон'юнктивітом, появою своєрідних плям на слизовій оболонці ротової порожнини (плями Копліка) і папульозно-плямистим висипом на шкірі. Це одне з найбільш заразних вірусних захворювань, його індекс контагіозності наближався до 100 % у довакцинальний період, найбільш сприятливими до кору були діти, тому відносили хворобу до так званих дитячих інфекційних хвороб.",
                note="Без ускладнень"
            });
            dataSet.Discases.Add(new Discase("Безсоння",
               dataSet.DicaseTypes
                   .First(e => e.Nazva == "Нервової сиситеми"))
            {

                Id = 6,
                Nazva = "Безсоння",
                Descripotion = "Безсо́ння або нічниця[1] (англ. insomnia) — порушення сну, зумовлене ослабленням гальмівного процесу в корі головного мозку. Є частим симптомом багатьох хвороб."

            });

            dataSet.Discases.Add(new Discase("Кома",
              dataSet.DicaseTypes
                  .First(e => e.Nazva == "Нервової сиситеми"))
            {

                Id = 7,
                Nazva = "Кома",
                Descripotion = "Ко́ма (грец. κῶμα — глибокий сон) — патологічний стан організму, що характеризується повною втратою свідомості, розладом життєво важливих функцій — кровообігу, дихання, обміну речовин, відсутністю рефлексів, реакції на подразники. Виникає гальмування функцій кори головного мозку, потім підкіркових утворень."
             
            });
            dataSet.Discases.Add(new Discase("Кардіоміопатія",
             dataSet.DicaseTypes
                 .First(e => e.Nazva == "Кровоносної системи"))
            {

                Id = 8,
                Nazva = "Кардіоміопатія",
                Descripotion = "Кардіоміопаті́я(міокардіопатія) — ураження серцевого м'язу нез'ясованої етіології.Нерідко кардіоміопатію доводиться диференціювати від ідіопатичного міокардиту Фідлера, який супроводжується виразнішими загальними запальними проявами.При ретельнішому опитуванні вдається виявити роль деяких факторів розвитку кардіоміопатії(наприклад, тривале зловживання алкоголем)."

            });



        }
        public static void CreatePatcient(IDateSet dataSet) {
            dataSet.Patcients.Add(new Patcient("Петренко", System.DateTime.Today, "DD",
                dataSet.Discases
                    .First(e => e.Nazva == "Вітрянка"))
            {
                Id = 1,
                Medical_card = "435",
                number_Chamber = "34",
                Sorname = "Петренко",
                Doctor = "Биков",
                Date = new System.DateTime(2001,03,05),

            });
            dataSet.Patcients.Add(new Patcient("Коваленк", System.DateTime.Today, "1",
               dataSet.Discases
                   .First(e => e.Nazva == "Кір"))
            {
                 Id = 2,
               Sorname = "Коваленк",
                Date = new System.DateTime(1991, 03, 01),
                Doctor = "Лобанов",
                Medical_card = "12345",
                number_Chamber = "76",

            });
            dataSet.Patcients.Add(new Patcient("Заболотний", System.DateTime.Today, "",
               dataSet.Discases
                   .First(e => e.Nazva == "Кома"))
            {
                 Id = 3,
               Sorname = "Заболотний",
                Date = new System.DateTime(2011, 05, 05),
                Doctor = "Чорнус",
                Medical_card = "67",
                number_Chamber = "89",

            });
            dataSet.Patcients.Add(new Patcient("Іванов", System.DateTime.Today, "",
               dataSet.Discases
                   .First(e => e.Nazva == "Кома"))
            {
                Id = 3,
                Sorname = "Іванов",
                Date = new System.DateTime(2011, 05, 06),
                Doctor = "Чорнус",
                Medical_card = "67",
                number_Chamber = "89",

            });


        }
    }
    }

