namespace DatabaseProjekt.App;

internal class Program
{
    static void Main(string[] args)
    {
       while(true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine( " Välkommen till skolan \n välj 1: för att se hur många lärare som jobbar i detta avdelning " +
                "\n Välj 2: för att lägga in nya personal \n Välj 3: för att spara ner nya elever \n Välj 4: för att spara ner betyg och vilken kurs de har läst" +
                " \n Välj 5: Hur många elever och i vilken avdelning jobbar \n Välj 6: Vissa info om alla elever \n Välj 7: Vissa alla aktiva kurser" +
                " \n Välj 8: Hur mycket betalar avdelning ut i lön i varje månad \n Välj 9: Hur mycket medellönen för olika avdelning ");

            int inpu;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    inpu = number;
                    break;
                }
                else
                {
                    Console.WriteLine("Ogiltigt inmatning");
                }
            }
            switch(inpu)
            {
                case 1:
                    App MyApp1 = new App();
                    MyApp1.AllStaffsInfo();
                    break;
                case 2:
                    App MyApp2 = new App();
                    MyApp2.SavNewStaffs();
                    break;
                case 3:
                    App MyApp3 = new App();
                    MyApp3.SaveAllStudents();
                    break;
                case 4:
                    App MyApp4 = new App();
                    MyApp4.SaveAllGrades();
                    break;
                case 5:
                    App MyApp5 = new App();
                    MyApp5.GetTeacherCountsByDepartment();
                    break;
                case 6:
                    App MyApp6 = new App();
                    MyApp6.GettInfoAboutAllStudents();
                    break;
                case 7:
                    App MyApp7 = new App();
                    MyApp7.GettAllActiceCourses();
                    break;
                case 8:
                    App MyApp8 = new App();
                    MyApp8.GetAllDepartmentSalary();
                    break;
                case 9:
                    App MyApp9 = new App();
                    MyApp9.GetAllAverageSalary();
                    break;
            }
        }
    }
}
