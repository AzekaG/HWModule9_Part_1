using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp1.Program;

namespace ConsoleApp1

{/*Создайте класс «Кредитная карточка». Класс должен
содержать:
■ Номер карты;
■ ФИО владельца;
■ Срок действия карты;
■ PIN;
■ Кредитный лимит;
■ Сумма денег.
Создайте необходимый набор методов класса. Реа-
лизуйте события для следующих ситуаций:
■ Пополнение счёта;
■ Расход денег со счёта;
■ Старт использования кредитных денег;
■ Достижение заданной суммы денег;
■ Смена PIN.
ДОМАШНЕЕ ЗАДАНИЕ
2*/
    internal class Program
    {
        public delegate void DelegateCreditCardActions();
        public delegate CreditCard DelMenuCreditCard();
        public class CreditCard
        {

            int creditLimit;
            string NumberCard { set; get; }
            string FirstName { set; get; }
            string LastName { set; get; }
            string FatherName { set; get; }
            int Pin { set; get; }
            int Cvv { set; get; }
            int CreditLimit  { set; get; }
            
            int Balance { set; get; }
            public CreditCard()
            {
                NumberCard = string.Empty;
                FirstName = string.Empty;
                LastName = string.Empty;
                FatherName = string.Empty;
            }
        
            public CreditCard(string firstName, string lastName, string fatherName, int pin, int cvv, int balance, string numberCard)
            {
                FirstName = firstName;
                LastName = lastName;
                FatherName = fatherName;
                Pin = pin;
                Cvv = cvv;

                Balance = balance;
                NumberCard = numberCard;

            }
            public void Top_up()
            {
                Console.WriteLine("Enter a Summ for Top-up : ");
                int topUp = int.Parse(Console.ReadLine());
                   if (creditLimit - CreditLimit > topUp) CreditLimit += topUp;
                    else { CreditLimit += topUp; Balance -= (creditLimit - CreditLimit); CreditLimit = creditLimit; }
                 


            }
            public void Expence()
            {
                int tempExpence = 0;
                do
                {
                    ShowBalance();
                    Console.WriteLine("Enter a Summ of expence : " + "\n  or for exit enter 0");
                    tempExpence = int.Parse(Console.ReadLine());
                    try
                    {
                        if (tempExpence > Balance + CreditLimit) throw new Exception("Not Enougth money");
                        else if (tempExpence > Balance)
                        {
                            CreditLimit -= (tempExpence - Balance);
                            Balance = 0;
                        }
                        else Balance -= tempExpence;
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                } while (tempExpence != 0);
            }
            void ChangePinCode()
            {
                Console.WriteLine("Enter old PinCode : ");
                int tempPin = int.Parse(Console.ReadLine());
                if (tempPin != Pin) throw new InvalidOperationException("Incorrect Pin");
                Console.WriteLine("enter a new pin : ");
                Pin = int.Parse(Console.ReadLine());
            }
            void ChangeCreditLimit()
            {
                Console.WriteLine("Enter a CreditLimit : ");
                CreditLimit = int.Parse(Console.ReadLine());
                creditLimit = CreditLimit;
                if (CreditLimit < 0)
                {
                    CreditLimit = 0;
                    throw new InvalidOperationException("Incorrect CreditLimit.");
                }
            }
            void ShowBalance()
            {
                Console.WriteLine("Balance : " + Balance + "\nCredit Limit : " + CreditLimit);
            }

            class InterfaceClient
            {
                InterfaceClient(ref CreditCard creditCard)
                {
                    if (creditCard.FirstName!=string.Empty)
                    {
                        creditCard = Menu_Filled_Card(creditCard);
                    }
                    else
                    {
                        Console.WriteLine("Enter an information : ");
                        creditCard = Menu_Empty_Card();
                    }

                }
                CreditCard Menu_Empty_Card()
                {
                    bool temp = false;
                    CreditCard creditCard = new CreditCard();
                    Console.WriteLine("Enter a first name : ");
                    creditCard.FirstName = Console.ReadLine();
                    Console.WriteLine("Enter a last name : ");
                    creditCard.LastName = Console.ReadLine();
                    Console.WriteLine("Enter a Father name : ");
                    creditCard.FatherName = Console.ReadLine();

                    do
                    {
                        try
                        { 
                            Console.WriteLine("Enter a NumberCard , Enter 16 numbers :");
                            creditCard.NumberCard = Console.ReadLine();
                            if ((creditCard.NumberCard.Length != 16 || !(double.TryParse(creditCard.NumberCard, out _))))
                                throw new InvalidOperationException("Incorrect Number. Enter 16 numbers.");
                            
                            Console.WriteLine("Enter a pin Code , Enter 4-6 numbers:");
                            creditCard.Pin = int.Parse(Console.ReadLine());
                            if (creditCard.Pin < 999 || creditCard.Pin > 999999)
                                throw new InvalidOperationException("Incorrect Pin. Enter 4-6 numbers.");

                            Console.WriteLine("Enter a balance >=0: ");
                            creditCard.Balance = int.Parse(Console.ReadLine());
                            if (creditCard.Balance < 0)
                                throw new InvalidOperationException("Incorrect Balance.");


                            temp = true;
                        }

                        catch (InvalidOperationException ex) { Console.WriteLine(ex.Message);
                            temp = false;
                        };
                    }while (!temp);
                    
                    return Menu_Filled_Card(creditCard);
                }
                CreditCard Menu_Filled_Card(CreditCard creditCard)
                {
                    bool temp = true;
                    do
                    {
                        
                        Console.WriteLine("Choose an action : " +
                                        "\n1. Top_Up Card" +
                                        "\n2. Make Expence" +
                                        "\n3. Change Pin Code" +
                                        "\n4. Start using  CreditLimit" +
                                        "\n5. Show Balance"+
                                        "\n0. Exit");
                        int choice = int.Parse(Console.ReadLine());
                        Console.Clear();
                        
                        DelegateCreditCardActions[] del = { creditCard.Top_up, creditCard.Expence, creditCard.ChangePinCode, 
                            creditCard.ChangeCreditLimit,creditCard.ShowBalance };
                        
                        try
                        {
                            if (choice == 0) temp = false;
                           
                            if (choice > 0 && choice < 6) del[choice-1].Invoke();
                            else if(temp== true) throw new InvalidOperationException("Incorrect choice");
                            

                        }
                        catch (InvalidOperationException ex) { Console.WriteLine(ex.Message); }
                      
                    } while (temp);
                    return creditCard;
                }
                static void Main(string[] args)
                {
                    CreditCard creditCard;
                    Console.WriteLine("Choose card for using : 1 - Default or 2 - Empty");
                    switch (int.Parse(Console.ReadLine()))
                    {
                        case 1:
                            {
                                creditCard = new CreditCard("Sergii", "Matviichuk", "Mihaylovich", 4444, 233, 10000, "12345678912345");
                                
                            }
                            break;
                        default:
                            { creditCard = new CreditCard(); }
                            break;
                    }
                    InterfaceClient IC = new InterfaceClient(ref creditCard) ;

                }

            }
        }
    } }
