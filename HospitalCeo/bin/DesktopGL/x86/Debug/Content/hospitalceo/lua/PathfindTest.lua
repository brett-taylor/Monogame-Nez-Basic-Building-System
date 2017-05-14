buildings = {}
buildings[1] = Building.Create("wall", 8, 10, 18, 1)
buildings[2] = Building.Create("wall", 26, 10, 1, 10)
buildings[3] = Building.Create("wall", 8, 19, 18, 1)
buildings[4] = Building.Create("wall", 8, 11, 1, 4)
buildings[5] = Building.Create("wall", 8, 16, 1, 3)
buildings[6] = Building.Create("wall", 17, 11, 1, 4)
buildings[7] = Building.Create("wall", 19, 14, 7, 1)
buildings[8] = Building.Create("wall", 9, 16, 7, 1)
buildings[9] = Building.Create("wall", 17, 16, 1, 3)
buildings[10] = Building.Create("floor", 9, 11, 8, 5)
buildings[11] = Building.Create("floor", 18, 11, 8, 3)
buildings[12] = Building.Create("floor", 18, 15, 8, 4)
buildings[13] = Building.Create("floor", 9, 17, 8, 2)
buildings[14] = Building.Create("floor", 8, 15, 1, 1)
buildings[15] = Building.Create("floor", 16, 16, 1, 1)
buildings[16] = Building.Create("floor", 17, 15, 1, 1)
buildings[17] = Building.Create("floor", 18, 14, 1, 1)

Util.Timer("FinishConstruction", 1000)
Util.Timer("StartWorkers", 1300)
Util.Timer("StartWorkers", 2000)
Util.Timer("StartWorkers", 3000)
Util.Timer("StartWorkers", 4000)
Util.Timer("StartWorkers", 5000)
Util.Timer("StartWorkers", 6000)
Util.Timer("StartWorkers", 7000)
Util.Timer("StartWorkers", 8000)
Util.Timer("StartWorkers", 9000)
Util.Timer("StartWorkers", 10000)

function FinishConstruction()
	for i = 1, 17 do
		buildings[i].Finish();	  
	end

	Task.Clear();
end

function StartWorkers()
	workerOne = Mob.CreateWorker(1150, 1250)
	workerTwo = Mob.CreateWorker(1950, 1350)
	workerThree = Mob.CreateWorker(2250, 1700)
	workerFour = Mob.CreateWorker(650, 1550)
	CreateInstruction(workerOne)
	CreateInstruction(workerTwo)
	CreateInstruction(workerThree)
	CreateInstruction(workerFour)

	workerOne.SetCanTakeJobs(false);
	workerTwo.SetCanTakeJobs(false);
    workerThree.SetCanTakeJobs(false);
    workerFour.SetCanTakeJobs(false);
end

function GetTile()
	math.randomseed(os.time())
	area = math.random(1, 5)
	topLeftPosX = 0;
	topLeftPosY = 0;
	bottomRightPosX = 0;
	bottomRightPosY = 0;

	if area == 1 then
		topLeftPosX = 9;
		topLeftPosY = 11;
		bottomRightPosX = 14;
		bottomRightPosY = 14;
	elseif area == 2 then
		topLeftPosX = 18;
		topLeftPosY = 11;
		bottomRightPosX = 25;
		bottomRightPosY = 13;
	elseif area == 3 then
		topLeftPosX = 18;
		topLeftPosY = 15;
		bottomRightPosX = 25;
		bottomRightPosY = 18;
	elseif area == 4 then
		topLeftPosX = 9;
		topLeftPosY = 17;
		bottomRightPosX = 16;
		bottomRightPosY = 18;
	else
		topLeftPosX = 5;
		topLeftPosY = 14;
		bottomRightPosX = 7;
		bottomRightPosY = 16;
	end

	actualPosX = math.random(topLeftPosX, bottomRightPosX);
	actualPosY = math.random(topLeftPosY, bottomRightPosY);
	tile = World.GetTileAt(actualPosX, actualPosY);
	if tile == nil then
		Util.Log("Tile not valid")
		tile = World.GetTileAt(9, 17)
	end

	return tile
end

function CreateInstruction(worker)
	Task.GiveWorkerInstruction(worker, GetTile(), 1, "CreateInstruction")
end