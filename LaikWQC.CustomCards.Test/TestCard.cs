using LaikWQC.CustomCards.Model;
using LaikWQC.CustomCards.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;

namespace LaikWQC.CustomCards.Test
{
    public class TestCard
    {
        public static void Test(Window owner)
        {
            var noemptystring = "!";
            var nomatterstring = "";
            var threenolessstring = "fuc";

            int? nonullint = 17;
            int? nomatterint = null;
            int? thouthousandint = 999;

            double? doubleInRange = 150;

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

            DateTime? dateValue = DateTime.Now;

            var properies = new List<ICustomProperty>()
            {
                new TestProperty("только что добавленная новая Проперти", noemptystring, x=>noemptystring=x, ConditionType.NoEmpty),
                CustomProperty.CreateStringProperty("не пустая строка:", noemptystring, x=>noemptystring=x, ConditionType.NoEmpty),
                CustomProperty.CreateStringProperty("любая строка:", nomatterstring, x=>nomatterstring=x, ConditionType.NoCondition),
                CustomProperty.CreateStringProperty("длиной не меньше 3 строка:", threenolessstring, x=>threenolessstring=x, x=> x.Length >=3),
                CustomProperty.CreateDoubleProperty("между 100 и 200 double", doubleInRange, x=>doubleInRange = x, x=> x>=100 && x<=200),
                CustomProperty.CreateDateProperty("дата", dateValue, x=>dateValue = x, ConditionType.NoEmpty),
                CustomProperty.Extra.CreateExpander("Экспандер", true, new List<ICustomProperty>()
                    {
                        CustomProperty.CreateIntProperty("не нулевой инт:", nonullint, x=>nonullint=x, ConditionType.NoEmpty),
                        CustomProperty.CreateIntProperty("любой инт:", nomatterint, x=>nomatterint=x, ConditionType.NoCondition),
                        CustomProperty.CreateIntProperty("меньше 1к инт:", thouthousandint, x=>thouthousandint=x, x => x<1000),
                        CustomProperty.Extra.CreateSeparator(),
                        CustomProperty.CreateCollectionProperty("коллекция классов:", number,x=>number = x, numbers, x=>x.Name, ConditionType.NoEmpty),
                        CustomProperty.CreateCollectionProperty("коллекция строк:", car,x=>car = x, cars, x=>x),
                        CustomProperty.CreateCollectionProperty("коллекция интов:", intNumber,x=>intNumber = x, intNumbers, x=>x.ToString()),
                        CustomProperty.CreateEnumProperty("енумы", enumValue, x=>enumValue = x ),
                        CustomProperty.CreateEnumProperty("енумы", enumValue, x=>$"{x.GetType().Name} {x}", x=>enumValue = x )
                    })
            };
            var cc = new CustomCardModel(properies).SetConfirmButtonText("Применить").SetCancelButtonText("Отмена")
                .SetConfirmCallback(() =>
                {
                    //колбек после нажатия кнопки "применить" (происходит после применения всех сеттеров)
                })
                .SetCancelCallback(() =>
                {
                    //колбек после нажатия кнопки "отмена"
                });
            var result = cc.IsConfirmed; //колбеки хороши, когда показываешь окно, но в диалоге можно воспользоваться и этим свойством
            WpfCustomCardService.ShowDialog(cc, "Test", owner);
        }

        public class Number
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }

        public enum TestEnum
        {
            [Display(Name = "Один")] one,
            [Display(Order = 0)] two,
            [Display(Name = "Три")] three
        }
    }

    //Можно создать свою Проперти и добавить в приложение библиотеку, описывающую ее view
    public class TestProperty : CustomProperty<string>, ICustomProperty
    {
        public TestProperty(string header, string value, Action<string> setter, Func<string, bool> correctCondition = null) : base(header, value ?? "", setter, correctCondition) { }

        public TestProperty(string header, string value, Action<string> setter, ConditionType conditionType) : base(header, value ?? "", setter)
        {
            switch (conditionType)
            {
                case ConditionType.NoCondition:
                    _correctCondition = null;
                    break;
                case ConditionType.NoEmpty:
                    _correctCondition = x => !string.IsNullOrEmpty(x);
                    break;
            }
        }
    }
}
