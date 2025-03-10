using System;
using Shop.API.Entities.Category;
using Shop.API.Entities.Product;

namespace Shop.API.Data
{
    public class RandomData
    {
        private readonly List<string> _nameSyllables;
        private readonly List<string> _imageUrls;
        private readonly List<string> _descriptions;
        private readonly HashSet<string> _stockKeepingUnits;
        public  Random RandomObj;
        public RandomData()
        {
            RandomObj = new Random();
            _stockKeepingUnits = [];

            while (_stockKeepingUnits.Count < 1000)
            {
                _stockKeepingUnits.Add( RandomObj.Next( 1_000_000, 9_999_999).ToString() );
            }
            _nameSyllables = [
                " "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," ",
                "ма", "ми", "му", "па", "пе", "по", "ра", "ре", "ро", "са", "се", "со", 
                "та", "те", "то", "ва", "ве", "во", "на", "не", "но", "ка", "ке", "ко", 
                "ла", "ле", "ло", "ба", "бе", "би", "бу", "жа", "же", "зи", "зу", "ча", 
                "че", "чи", "цу", "ша", "ше", "ши", "цу", "да", "де", "ди", "ду", "та", 
                "те", "ти", "ту", "ка", "ке"
                ];
            _imageUrls = [
                "https://timeweb.com/media/articles/0001/18/thumb_17628_articles_standart.png",
                "https://timeweb.com/media/articles/0001/18/thumb_17629_articles_standart.png",
                "https://timeweb.com/media/articles/0001/18/thumb_17638_articles_standart.png",
                "https://i.pinimg.com/736x/a7/da/f2/a7daf24393f0ba0d8282d9e720e88510.jpg",
                "https://wallpaper.forfun.com/fetch/74/74d2c5f8c46325771cab9ac7613fb04f.jpeg?h=900&r=0.5",
                "https://cdn1.ozone.ru/s3/multimedia-o/6063449232.jpg",
                "https://timeweb.com/media/articles/0001/18/thumb_17634_articles_standart.png",
                "https://timeweb.com/media/articles/0001/18/thumb_17631_articles_standart.png",
                "https://random-image-pepebigotes.vercel.app/api/skeleton-random-image",
                "https://random-image-pepebigotes.vercel.app/api/skeleton-random-image",
                "https://random-image-pepebigotes.vercel.app/api/skeleton-random-image",
                "https://random-image-pepebigotes.vercel.app/api/skeleton-random-image",
                "https://random-image-pepebigotes.vercel.app/api/skeleton-random-image",
                "https://random-image-pepebigotes.vercel.app/api/skeleton-random-image"
            ];
            _descriptions =
            [
                    @"#Шоколадный мусс с карамелью# Шоколадный мусс – это нежнейший десерт,  способный покорить любого сладкоежку.![Alt text](16/9)(https://random-image-pepebigotes.vercel.app/api/skeleton-random-image) Густая текстура из темного шоколада идеально сочетается с тонким слоем карамели, добавляющим сладкую нотку. Каждый ложечный вдох наполнен насыщенным вкусом какао и легкой горчинкой. Сверху десерт украшен тонкой крошкой горького шоколада, создающей приятный контраст текстур. Подходит как для праздничного ужина, так и для уютного вечера с чашкой чая.",

                    @"#Ванильный макарун с лимонным кремом# Миниатюрное печенье с хрустящей корочкой и мягкой сердцевиной – настоящий символ французского шика.![Alt text](16/9)(https://random-image-pepebigotes.vercel.app/api/skeleton-random-image) Лимонный крем с легкой кислинкой обрамляет сладость ванильного теста, создавая изысканный баланс. Каждый кусочек тает во рту, оставляя приятное цитрусовое послевкусие. Идеально подходит к утреннему кофе или как легкий десерт после обеда.",

                    @"#Трюфельные конфеты с орехами# Эти трюфельные конфеты – настоящее сокровище для любителей шоколада.![Alt text](16/9)(https://random-image-pepebigotes.vercel.app/api/skeleton-random-image) Тонкая хрустящая оболочка скрывает кремовую начинку, приготовленную из бельгийского шоколада. Кусочки орехов добавляют текстурное разнообразие и легкий хруст. Каждая конфета – это момент удовольствия, идеальный для неспешного наслаждения.",

                    @"#Ягодный чизкейк# Чизкейк на основе сливочного сыра с тонким слоем ягодного соуса – это нежный и освежающий десерт.![Alt text](16/9)(https://random-image-pepebigotes.vercel.app/api/skeleton-random-image) Хрустящее печенье, служащее основой, отлично дополняет насыщенный сливочный вкус. Ягоды придают легкую кислинку, которая делает десерт особенно утонченным. Подходит для любых мероприятий – от домашних посиделок до торжественных праздников.",

                    @"#Эклеры с заварным кремом# Эклеры – это классика французской кондитерской традиции. Воздушное тесто, начиненное нежным заварным кремом, буквально тает во рту.![Alt text](16/9)(https://random-image-pepebigotes.vercel.app/api/skeleton-random-image) Сверху эклеры покрыты глазурью, которая добавляет сладость и завершенность. Лакомство идеально подходит для чаепитий или небольших праздников.",

                    @"#Фисташковый торт с белым шоколадом# Этот торт – настоящая симфония вкусов. Фисташковый бисквит с насыщенным ореховым ароматом обрамлен кремом из белого шоколада.![Alt text](16/9)(https://random-image-pepebigotes.vercel.app/api/skeleton-random-image) Сверху десерт украшен дроблеными фисташками и легкой сахарной пудрой. Легкий, но насыщенный вкус делает его любимцем гостей любого праздника.",

                    @"#Рахат-лукум с розовой водой# Кубики рахат-лукума – это воплощение восточной сладости. Мягкая текстура, пропитанная ароматом розовой воды, буквально тает во рту.![Alt text](16/9)(https://random-image-pepebigotes.vercel.app/api/skeleton-random-image) Сверху каждый кусочек обсыпан сахарной пудрой, создавая приятный контраст сладости и аромата. Угощение прекрасно сочетается с крепким чаем или кофе.",

                    @"#Медовик с карамельной пропиткой# Слоеный медовик с мягкой карамельной пропиткой – это десерт, от которого невозможно отказаться. Каждый слой теста пропитан нежным кремом, а карамельная нотка делает вкус особенно глубоким.![Alt text](16/9)(https://random-image-pepebigotes.vercel.app/api/skeleton-random-image) Идеален для семейных праздников и уютных вечеров.",

                    @"#Лимонный тарт с меренгой# Лимонный тарт – это изысканное сочетание кислинки лимонного крема и сладкой воздушной меренги. Хрустящая основа из песочного теста идеально дополняет текстурную композицию.![Alt text](16/9)(https://random-image-pepebigotes.vercel.app/api/skeleton-random-image) Каждый кусочек дарит бодрящее послевкусие и легкость. Подходит как для легкого перекуса, так и для особенных случаев.",

                    @"#Шоколадный фондан с жидкой начинкой# Фондан – это десерт, который покоряет с первого укуса. Хрустящая корочка скрывает внутри густую жидкую начинку из темного шоколада.![Alt text](16/9)(https://random-image-pepebigotes.vercel.app/api/skeleton-random-image) Подается с шариком ванильного мороженого, который подчеркивает глубину шоколадного вкуса. Настоящий шедевр для ценителей сладкого.",
                    @"#Вот вам яркий пример современных тенденций# \tНовая __модель__ организационной деятельности обеспечивает широкому кругу (специалистов) участие в формировании соответствующих условий активизации.![Alt text](16/9)(https://random-image-pepebigotes.vercel.app/api/skeleton-random-image) Учитывая ключевые сценарии поведения, экономическая повестка сегодняшнего дня предполагает независимые способы реализации переосмысления внешнеэкономических политик. \nТаким образом, существующая теория напрямую зависит от экономической целесообразности принимаемых решений. Внезапно, интерактивные прототипы освещают чрезвычайно интересные особенности картины в целом, однако конкретные выводы, разумеется, призваны к ответу. Современные технологии достигли такого уровня, что начало повседневной работы по формированию позиции не даёт нам иного выбора, кроме определения первоочередных требований. Но семантический разбор внешних противодействий позволяет выполнить важные задания по разработке переосмысления внешнеэкономических политик."
            ];
        }
        public string GetRandomName()
        {
            return string.Join("", RandomObj.GetItems(_nameSyllables.ToArray(), RandomObj.Next(3,13)));
        }
        public string[] GetRandomImageUrls()
        {
            return RandomObj.GetItems(_imageUrls.ToArray(), RandomObj.Next(2,5));
        } 
        public string GetRandomCategory(List<Category> categories)
        {
            return categories[ RandomObj.Next(0, categories.Count - 1) ].Name;
        }
        public string GetRandomAbout()
        {
            return _descriptions[ RandomObj.Next(0, _descriptions.Count - 1) ];
        }
        public string GetRandomSKU()
        {
            var result = _stockKeepingUnits.Last();

            _stockKeepingUnits.Remove(result);

            return result;
        }
    }
}