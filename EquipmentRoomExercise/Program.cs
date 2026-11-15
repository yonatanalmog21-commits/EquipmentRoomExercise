using System;

// <summary>
//Todo: צרו מחלקת חריגים המטפלת במקרים בהם אין מספיק מחשבים זמינים להשאלה.
//שם המחלקה צריך להיות EquipmentUnavailableException.

public class EquipmentUnavailableException : Exception
{
    public int RequestedAmount { get; set; }
    public int MissingAmount { get; set; }

    public EquipmentUnavailableException(int requestedAmount, int missingAmount)
        : base($"Not enough computers available to borrow. Requested: {requestedAmount}, Missing: {missingAmount}")
    {
        RequestedAmount = requestedAmount;
        MissingAmount = missingAmount;
    }



    class EquipmentRoom
    {
        private int availableComputers;

        public int AvailableComputers
        {
            get
            {
                return availableComputers;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Available computers cannot be negative.");
                }
                availableComputers = value;
            }
        }

        public EquipmentRoom(int amount)
        {
            AvailableComputers = amount;
        }

        public void Borrow(int amount)
        {

            // TODO: יש לזרוק חריגה מתאימה כאשר אין מספיק מחשבים זמינים.
            if (availableComputers < amount)
            {
                throw new InvalidOperationException("Not enough computers available to borrow.");
            }




            AvailableComputers -= amount;
            Console.WriteLine($"Borrowed successfully {amount} computers.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int initialComputers = 10;

            EquipmentRoom room = new EquipmentRoom(initialComputers);

            // יצירת חדר מחשבים עם מספר מחשבים שלילי:
            try
            {
                EquipmentRoom invalidRoom = new EquipmentRoom(-5);
                room.Borrow(4);
                Console.WriteLine("continue on...");

                room.Borrow(8);
                invalidRoom.Borrow(4);
            }
            catch (EquipmentUnavailableException ex)
            {
                Console.WriteLine($"Error creating room: {ex.Message}");
            }

            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Error creating room: {ex.Message}");
            }
            // לאחר מימוש הבדיקה ב-set, יש לוודא שמצב כזה מטופל כראוי.
            // TODO: יש להוסיף try-catch מתאימים סביב הקריאות שעלולות לזרוק חריגה.
            // int negativeComputers = -5;
            // EquipmentRoom invalidRoom = new EquipmentRoom(negativeComputers);





            Console.WriteLine("Availble computers " + room.AvailableComputers);
        }
    }
}
