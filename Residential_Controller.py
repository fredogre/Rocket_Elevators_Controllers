import time

class elevator: 
    floorList = []
    def __init__(self, status, direction, floorNumber, elevatorNumber):
       self.status = status
       self.direction = direction
       self.floorNumber = floorNumber
       self.elevatorNumber = elevatorNumber
    
    def addToFloorList(self, floorNumber):
        self.floorList.append(floorNumber)
        self.sortFloorList()       

    def sortFloorList(self):
        if self.direction is str("up"):
            self.floorList.sort()
        else:
            self.floorList.sort(reverse=True)

class floor:
    def __init__(self, floorNumber, buttons):
        self.floorNumber = floorNumber
        self.directionButtons = buttons

class directionButtons:
    directionButtonList = []
    def __init__(self, direction, floorNumber):
        self.direction = direction
        self.floor = floorNumber
      

class column:
    elevators = []
    def __init__(self, elevators, floors):
        self.elevators = elevators
        self.floors = floors
        

class floorButtons:
    floorButtons = []
    def __init__(self, elevator, requestedFloor, activated):
        self.elevator = elevator
        self.requestedFloor = requestedFloor
        self.activated = activated
        
class openDoorButtons:
    def __init__(self, elevator):
        self.elevator = elevator

class closeDoorButtons:
    def __init__(self, elevator):
        self.elevator = elevator

class door: 
    def __init__(self, elevator, status):
        self.elevator = elevator
        self.status = status

class elevatorController:    
    def __init__(self, numberOfFloor, numberOfElevator, elevators):
        self.numberOfFloor = numberOfFloor
        self.numberOfElevator = numberOfElevator
        floors = []
        self.column = column(elevators, floors)

    def requestElevator(self, floorNumber, direction):
        print("requestElevator")
        print("Request for Elevator on floor = " + str(floorNumber))
        print("for direction = " + str(direction))
        elevator = self.findElevator(floorNumber, direction)
        elevator.addToFloorList(floorNumber)
        print("floorlist = " + str(elevator.floorList))
        
        print("REQUESTED elevator found = " + str(elevator.elevatorNumber))
        self.operateElevator(elevator)
        return elevator
        
    def findElevator(self, floorNumber, direction):
        print("findElevator  for floorNumber = " + str(floorNumber))
        print("findElevator  for direction = " + str(direction))
        print("column elevators  = " + str(self.column.elevators))

        for elevator in self.column.elevators: 
            if floorNumber == elevator.floorNumber and elevator.status == "stopped" and elevator.direction == direction:
                print("elevator found is on floor = " + str(elevator.floorNumber))
                return elevator
            if floorNumber == elevator.floorNumber and elevator.status =="idle":
                print("elevator found is on floor = " + str(elevator.floorNumber))
                return elevator
            if direction == "up" and elevator.direction == direction and floorNumber > elevator.floorNumber:
                print("elevator found is on floor = " + str(elevator.floorNumber))
                return elevator
            if direction == "down" and elevator.direction == direction and floorNumber < elevator.floorNumber:
                print("elevator found is on floor = " + str(elevator.floorNumber))
                return elevator
            if floorNumber != elevator.floorNumber and elevator.status == "idle":
                print("elevator found is on floor = " + str(elevator.floorNumber))
                return elevator
                

        print("sending elevator with shortest floorlist")
        return self.shortestFloorList()
        

    def requestFloor(self, elevator, requestedFloor):
        print("requestFloor")
        print("Inside elevator = " + str(elevator.elevatorNumber))
        print("The requested floor is = " + str(requestedFloor))
        elevator.addToFloorList(requestedFloor)
        print("floorlist = " + str(elevator.floorList))
        self.operateElevator(elevator)
    
    def shortestFloorList(self):
        shortestLength = 10
        shortestListElevator = None
        for elevator in self.column.elevators:
            if len(elevator.floorList) < shortestLength:
                shortestLength = len(elevator.floorList)
                shortestListElevator = elevator

        print("elevator with shortest floor list: " + shortestListElevator.elevatorNumber)

        return shortestListElevator

    def operateElevator(self, elevator):
        print("operating elevator " + str(elevator.elevatorNumber))
        if len(elevator.floorList) > 0:
            requestedFloor = elevator.floorList[0]
            if elevator.floorNumber == requestedFloor: 
                print("open doors elevator on floor " + str(elevator.floorNumber))
                self.OpenDoors(elevator)
            elif elevator.floorNumber > requestedFloor:
                print("move down elevator to requested floor " + str(elevator.floorNumber))
                self.moveDown(elevator)
            elif elevator.floorNumber < requestedFloor:
                print("move up elevator to requested floor " + str (elevator.floorNumber))
                self.moveUp(elevator)

        else:
             elevator.status = "idle"
             print("elevator status is " + str(elevator.status))

    def moveDown(self, elevator):
        elevator.status = "moving"
        print("elevator status is " + str(elevator.status))
        requestedFloor = elevator.floorList[0]
        while (elevator.floorNumber > requestedFloor): 
           elevator.floorNumber = elevator.floorNumber - 1
           print("moved down to floor", elevator.floorNumber)
        self.OpenDoors(elevator)
        
    def moveUp(self, elevator):
        elevator.status = "moving"
        print("elevator status is " + str(elevator.status))
        requestedFloor = elevator.floorList[0]
        while (elevator.floorNumber < requestedFloor):
            elevator.floorNumber = elevator.floorNumber + 1
            print("moved up to floor", elevator.floorNumber)
        self.OpenDoors(elevator)

    def OpenDoors(self, elevator): 
        elevator.status = "stopped"
        print("elevator status is " + str(elevator.status))
        print("opening doors for elevator " + str(elevator.elevatorNumber) + " on floor  " + str(elevator.floorNumber))
        time.sleep(5)
        elevator.floorList.remove(elevator.floorList[0])
        print("updated floorList = " + str(elevator.floorList))
        self.CloseDoors(elevator)

    def CloseDoors(self, elevator): 
        elevator.status = "stopped"
        print("elevator status is " + str(elevator.status))
        print("closing doors for elevator " + str(elevator.elevatorNumber))
        time.sleep(5)
        print("doors are closed for elevator "+ str(elevator.elevatorNumber))
        self.operateElevator(elevator)



        
        

        
        
#test 
#elevators = [elevator("idle", "none", 1, "one"), elevator("idle", "none", 4, "two")]
#controller = elevatorController(10, 2, elevators) 
#controller.requestElevator(3, "up")
#elevators[0].floorList = [5,6,9]
#elevators[1].floorList = [7,5]
#controller.requestFloor (elevators[0], 7)
#controller.operateElevator (elevators[1])


#test  
#elevators = [elevator("idle", "none", 1, "one"), elevator("idle", "none", 10, "two")]
#controller = elevatorController(10, 2, elevators) 
#elevator = controller.requestElevator(4, "up")
#if elevator.floorNumber == 4:
#    controller.requestFloor (elevator, 7)
#elevators[0].floorList = [5,6,9]
