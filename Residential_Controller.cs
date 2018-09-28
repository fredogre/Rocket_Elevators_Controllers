using System;
using System.Collections.Generic;

namespace Rocket_Elevators_Controllers
{
    class Program
    {
        static void Main(string[] args)
        {
        
        

        List<Elevator> ElevatorList = new List<Elevator>();
        Elevator elevator1 = new Elevator(1, 1);
        ElevatorList.Add(elevator1);

        ElevatorController Controller = new ElevatorController (10,2); 
        Console.WriteLine("Request elevator");
        Controller.RequestElevator(4, "up");
        Console.WriteLine("Request floor");
        Controller.RequestFloor(ElevatorList [0], 7 );
        
        }
    }

    class Elevator {
        public int floorNumber;
        public string status;
        public string direction; 
        public int elevatorNumber;
        public List<int> floorList;
        public Elevator(int floorNumber, int elevatorNumber)
    {   this.floorNumber = floorNumber; 
        this.status = "idle"; 
        this.direction = "GoingNowhere"; 
        this.elevatorNumber = elevatorNumber; 
        this.floorList = new List<int>();
    }
        public void AddRequestedFloorToFloorList(int requestedFloor){
            this.floorList.Add(requestedFloor);
        }

        public bool EmptyFloorList()
        {
            return floorList.Count==0?true:false;
        } 

    }
    
    class Floor {
        public int floorNumber;
        public List<DirectionButtons> buttons; 
       
        public Floor(int floorNumber, List<DirectionButtons> buttons)
    {   this.floorNumber = floorNumber; 
        this.buttons = buttons;
        }
    }

    class DirectionButtons {
        public string direction; 
        public int floorNumber; 
        public DirectionButtons(string direction, int floorNumber)
    {   this.direction = direction; 
        this.floorNumber = floorNumber;

    }
    }
    class Column {
        public int elevators; 
        public int floors; 
        public Column (int elevators, int floors)
   {    this.elevators = elevators; 
        this.floors = floors; 
   }
   }

   class FloorButtons {
       public int elevators; 
       public int requestedFloor; 
       public string activated; 
       public FloorButtons (int elevators, int requestedFloor, string activated)
    {  this.elevators = elevators; 
       this.requestedFloor = requestedFloor;
       this.activated = activated;
       }
   }
    class OpenDoorButtons {
      public int elevators;
      public OpenDoorButtons (int elevators)
    { this.elevators = elevators; 
      }
    }

    class CloseDoorButtons {
        public int elevaotors;
        public CloseDoorButtons (int elevators)
    { this.elevaotors = elevators;
     }
    }

    class Door {
        public int elevators; 
        public string status; 
        public Door(int elevators, string status)
    { this.elevators = elevators;
      this.status = status;
    }
    }

    class ElevatorController {
        public int numberOfFloor; 
        public int numberOfElevator;
        public List<int> floorList; 
        public List<Elevator> elevatorList;
        public ElevatorController(int numberOfFloor, int numberOfElevator)
    {   this.numberOfFloor = numberOfFloor; 
        this.numberOfElevator = numberOfElevator; 
        this.floorList = new List<int>();

        this.elevatorList = new List<Elevator> ();
           for (int i = 0; i < numberOfElevator; i++) {
               this.elevatorList.Add (new Elevator(1, i));
           }
    }


       public void RequestElevator(int floorNumber, string direction)
       {
           Console.WriteLine("Request Elevator On Floor " + floorNumber.ToString() + ", going " + direction);
        var elevator = FindElevator(floorNumber,  direction);   

          //RequestFloor(elevator, floorNumber); 
          elevator.AddRequestedFloorToFloorList(floorNumber);
          OperateElevator(elevator); 
       }

       public Elevator FindElevator(int floorNumber, string direction)
       { 
           foreach(Elevator elevator in this.elevatorList)
           { 
                if (floorNumber == elevator.floorNumber || elevator.status == "stopped" || elevator.direction == direction)
                {
                    return elevator; 
                }
                else if (floorNumber == elevator.floorNumber || elevator.status == "idle")
                {
                    return elevator; 
                }
                else if (direction == "up" || elevator.direction == direction || floorNumber < elevator.floorNumber)
                {
                    return elevator; 
                }
                else if (direction == "down" || elevator.direction == direction || floorNumber > elevator.floorNumber)
                {
                    return elevator; 
                }
                else if (floorNumber != elevator.floorNumber || elevator.status == "idle")
                {
                    return elevator; 
                }
           }
           return ShortestFloorList();
       }
       public void RequestFloor(Elevator elevator, int requestedFloor)
       {
           elevator.AddRequestedFloorToFloorList(requestedFloor);
           Console.WriteLine("Request For Floor Number " + requestedFloor);
           OperateElevator(elevator); 
       }
        
        public void MoveDown (Elevator elevator, int requestedFloor)
        {
            
                while (elevator.floorNumber > requestedFloor) {
                    elevator.floorNumber--; 
                }; 
            Console.WriteLine("Moved Down to Floor" + requestedFloor.ToString()); 

            OpenDoors(elevator, requestedFloor); 
            
        }

        public void MoveUp (Elevator elevator, int requestedFloor)
        { requestedFloor = elevator.floorList[0]; 
                while (elevator.floorNumber < requestedFloor) {
                    elevator.floorNumber++; 
                }; 
            
            Console.WriteLine ("Moving up to floor " + requestedFloor.ToString());

            OpenDoors(elevator, requestedFloor); 
        }

        public void OpenDoors (Elevator elevator, int requestedFloor)
        {
            elevator.floorList.Remove(requestedFloor);
            this.wait(5000);
        
            Console.WriteLine ("Opening Doors on floor " + requestedFloor.ToString()); 
    
            CloseDoors(elevator, requestedFloor);
        
        }

        public void CloseDoors (Elevator elevator, int requestedFloor)
        {
            this.wait(5000);
            Console.WriteLine ("Closing Doors on floor " + requestedFloor.ToString()); 

            OperateElevator(elevator); 
        }
       public void OperateElevator(Elevator elevator)
       {
           
           if(elevator.EmptyFloorList() == false)
            {
               var requestedFloor = elevator.floorList[0]; 
                if (elevator.floorNumber == requestedFloor)
            { 
                OpenDoors(elevator, requestedFloor); 
                Console.WriteLine ("Opening Doors  " + requestedFloor.ToString()); 
                
            }
            
                 else if (elevator.floorNumber > requestedFloor)
            {
                MoveDown(elevator, requestedFloor); 
                 

            }
                else if (elevator.floorNumber < requestedFloor)
            {   
                MoveUp(elevator, requestedFloor); 
                 
                }
            
           }

             else elevator.status = "idle"; 
        
       }

       public Elevator ShortestFloorList(){
           var ShortestLenght = 10; 
           Elevator ShortestListElevator = null; 
            foreach(Elevator elevator in this.elevatorList)
            { 
                if (elevator.floorList.Count < ShortestLenght) {
                    ShortestLenght = elevator.floorList.Count;
                    ShortestListElevator = elevator; 
                }
            }
            return ShortestListElevator; 
        }
            
        public List<int> SortedFloorList(List<int> floorList, string direction)
        {
               if (direction == "up") 
               {
                floorList.Sort(); 
               }
                else
                {
                floorList.Sort((a, b) => -1* a.CompareTo(b));
                }
               return floorList;

        }
            

      
    public void wait (int ms) {
        System.Threading.Thread.Sleep (ms);
      }
    }


    }

