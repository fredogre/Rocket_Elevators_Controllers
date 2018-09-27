using System;
using System.Collections.Generic;

namespace Rocket_Elevators_Controllers
{
    class Program
    {
        static void Main(string[] args)
        {
        List<int> floorList = new List<int>();
        floorList.Add(1);
        floorList.Add(2);
        floorList.Add(3);
        floorList.Add(4);
        floorList.Add(5);
        floorList.Add(6);
        floorList.Add(7);
        floorList.Add(8);
        floorList.Add(9);
        floorList.Add(10);

        List<Elevator> ElevatorList = new List<Elevator>();
        Elevator elevator1 = new Elevator(1, "idle", "none", 1);
        ElevatorList.Add(elevator1);



            Console.WriteLine("Hello World!");
        }
    }

    class Elevator {
        private int floorNumber;
        private string status;
        private string direction; 
        private int elevatorNumber;
        private List<int> floorList;
        public Elevator(int floorNumber, string status, string direction, int elevatorNumber)
    {   this.floorNumber = floorNumber; 
        this.status = status; 
        this.direction = direction; 
        this.elevatorNumber = elevatorNumber; 
        this.floorList = new List<int>();
    } 
    }
    
    class Floor {
        private int floorNumber;
        private List<DirectionButtons> buttons; 
       
        public Floor(int floorNumber, List<DirectionButtons> buttons)
    {   this.floorNumber = floorNumber; 
        this.buttons = buttons;
        }
    }

    class DirectionButtons {
        private string direction; 
        private int floorNumber; 
        public DirectionButtons(string direction, int floorNumber)
    {   this.direction = direction; 
        this.floorNumber = floorNumber;

    }
    }
    class Column {
        private int elevators; 
        private int floors; 
        public Column (int elevators, int floors)
   {    this.elevators = elevators; 
        this.floors = floors; 
   }
   }

   class FloorButtons {
       private int elevators; 
       private int requestedFloor; 
       private string activated; 
       public FloorButtons (int elevators, int requestedFloor, string activated)
    {  this.elevators = elevators; 
       this.requestedFloor = requestedFloor;
       this.activated = activated;
       }
   }
    class OpenDoorButtons {
      private int elevators;
      public OpenDoorButtons (int elevators)
    { this.elevators = elevators; 
      }
    }

    class CloseDoorButtons {
        private int elevaotors;
        public CloseDoorButtons (int elevators)
    { this.elevaotors = elevators;
     }
    }

    class Door {
        private int elevators; 
        private string status; 
        public Door(int elevators, string status)
    { this.elevators = elevators;
      this.status = status;
    }
    }

    class ElevatorController {
        private int numberOfFloor; 
        private int numberOfElevator;
        private int elevators;
        private List<int> floorList; 
        public ElevatorController(int numberOfFloor, int numberOfElevator, int elevators,List<int> floorList )
    {  this.numberOfFloor = numberOfFloor; 
       this.numberOfElevator = numberOfElevator; 
       this.elevators = elevators; 
       this.floorList = new List<int>();
    }
    }


    }

