using LaikWQC.CustomCards.Model;
using LaikWQC.CustomCards.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LaikWQC.CustomCards.Test
{
    public class TestCard
    {
        public static void Test()
        {
            var noemptystring = "!";
            var nomatterstring = "";
            var threenolessstring = "fuc";

            int? nonullint = 17;
            int? nomatterint = null;
            int? thouthousandint = 999;

            var numbers = new List<Number>()
            {
                new Number() {Name = "один", Value = 1},
                new Number() {Name = "два", Value = 2},
                new Number() {Name = "три", Value = 3},
                new Number() {Name = "четыре", Value = 4},
                new Number() {Name = "пять", Value = 5},
            };
            Number number = numbers[2];

            var cars = new List<string>()
            {
                "Машинка",
                "Грузовая машинка",
            };
            string car = null;

            var intNumbers = new List<int>() { 1, 2, 3, 4 };
            int intNumber = 21;

            var enumValue = TestEnum.two;

            //чтобы показать возможности, я продублировал property, но стоит иметь в виду, что дублирующие property перепишут значения (т.о. применится только то, что в экспандере)
            var properies = new List<ICustomProperty>()
                {
                    new CustomStringProperty("не пустая строка:", noemptystring, x=>noemptystring = x, ConditionType.NoEmpty),
                    new CustomStringProperty("любая строка:", nomatterstring, x=>nomatterstring=x, ConditionType.NoCondition),
                    new CustomStringProperty("длиной не меньше 3 строка:", threenolessstring, x=>threenolessstring=x, x=> x.Length >=3),
                    new CustomIntProperty("не нулевой инт:", nonullint, x=>nonullint=x, ConditionType.NoEmpty),
                    new CustomIntProperty("любой инт:", nomatterint, x=>nomatterint=x, ConditionType.NoCondition),
                    new CustomIntProperty("меньше 1к инт:", thouthousandint, x=>thouthousandint=x, x => x<1000),
                    new CustomSeparatorProperty(),
                    new CustomCollectionPropertyMock(new CustomCollectionProperty<Number>("коллекция классов:", number,x=>number = x, numbers, x=>x.Name, ConditionType.NoEmpty, NullElementType.MockElement)),
                    new CustomCollectionPropertyMock(new CustomCollectionProperty<string>("коллекция строк:", car,x=>car = x, cars, x=>x, ConditionType.NoEmpty, NullElementType.NoElement)),
                    new CustomCollectionPropertyMock(new CustomCollectionProperty<int>("коллекция интов:", intNumber,x=>intNumber = x, intNumbers, x=>x.ToString(), ConditionType.NoEmpty, NullElementType.NoElement)),
                    //нельзя добавить сам CustomCollectionProperty, только его Mock
                    //new CustomCollectionProperty<int>("коллекция интов:", intNumber,x=>intNumber = x, intNumbers, x=>x.ToString(), ConditionType.NoEmpty, NullElementType.NoElement),
                    CustomEnumProperty<TestEnum>.GetEnumProperty("енумы", enumValue, x=>enumValue = x),

                    CustomProperty.Extra.CreateExpander("Alternative calls", new List<ICustomProperty>()
                    {
                        CustomProperty.CreateStringProperty("не пустая строка:", noemptystring, x=>noemptystring=x, ConditionType.NoEmpty),
                        CustomProperty.CreateStringProperty("любая строка:", nomatterstring, x=>nomatterstring=x, ConditionType.NoCondition),
                        CustomProperty.CreateStringProperty("длиной не меньше 3 строка:", threenolessstring, x=>threenolessstring=x, x=> x.Length >=3),
                        CustomProperty.CreateIntProperty("не нулевой инт:", nonullint, x=>nonullint=x, ConditionType.NoEmpty),
                        CustomProperty.CreateIntProperty("любой инт:", nomatterint, x=>nomatterint=x, ConditionType.NoCondition),
                        CustomProperty.CreateIntProperty("меньше 1к инт:", thouthousandint, x=>thouthousandint=x, x => x<1000),
                        CustomProperty.Extra.CreateSeparator(),
                        CustomProperty.CreateCollectionProperty("коллекция классов:", number,x=>number = x, numbers, x=>x.Name, ConditionType.NoEmpty),
                        CustomProperty.CreateCollectionProperty("коллекция строк:", car,x=>car = x, cars, x=>x),
                        CustomProperty.CreateCollectionProperty("коллекция интов:", intNumber,x=>intNumber = x, intNumbers, x=>x.ToString()),
                        CustomProperty.CreateEnumProperty("енумы", enumValue, x=>enumValue = x )
                    })
            };
            var cc = new CustomCardModel(properies).SetConfirmButtonText("Применить").SetCancelButtonText("Отмена").SetConfirmCallback(()=> 
            {
                //колбек после нажатия кнопки "применить" (происходит после применения всех сеттеров)
            });
            WpfCustomCardService.Show(cc, "Test", 400, 400);
        }

        public class Number
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }

        public enum TestEnum
        {
            [Display(Name = "Один")] one,
            two,
            [Display(Name = "Три")] three
        }
    }
}
