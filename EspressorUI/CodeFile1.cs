using System;
using System.Collections.Generic;

namespace Espressorlibrary
{
    public enum State
    {
        Initial_state,
        Pot,
        Water,
        Coffee,
        Pot_preheated,
        Pot_Water,
        Pot_Coffee,
        Pot_preheated_Water,
        Pot_preheated_Coffee,
        Pot_preheated_Water_warmed,
        Pot_preheated_Water_Coffee,
        Pot_preheated_Water_warmed_Pressure,
        Pot_preheated_Water_warmed_Coffee,
        Pot_preheated_Water_warmed_Coffee_Pressure,
        Filtering,
        STOP,
        Pot_Water_warmed,
        Pot_Water_Coffee,
        Pot_Water_warmed_Pressure,
        Pot_Water_warmed_Coffee,
        Pot_Water_warmed_Coffee_Pressure,
        Water_Coffee,
        Water_warmed,
        Water_warmed_Coffee,
        Water_warmed_Coffee_Pressure,
        Water_warmed_Pressure
    }
    public enum Action
    {
        AddPot,
        RemovePot,
        AddWater,
        AddCoffee,
        PreheatPot,
        HeatWater,
        SetPressure,
        Start,
        Stop,
        RemoveCoffee,
        New,
        Continue
    }
 
    class Pot
    {
        public bool potInside { get; set; }
        public bool GetPotPosition()
        {
            return potInside;
        }
    }

    class Plate
    {
        public int potMass { get; set; }
        public int potTemperature { get; set; }

    }
    class Boiler
    {
        public int waterLevel { get; set; }
        public int waterTemperature { get; set; }


    }
    public class Espressor
    {
        Pot pot = new Pot { potInside = false };
        Plate plate = new Plate
        {
            potMass = 0,
            potTemperature = 0
        };
        Boiler boiler = new Boiler
        {
            waterLevel = 0,
            waterTemperature = 0
        };
        string indicatorLight="yellow";
        int coffeegrams = 0;
        int pressure = 1;
        Dictionary<State, List<Transition>> transitionMap;
        public Espressor()
        {
            transitionMap= new Dictionary<State, List<Transition>>();
            FillTransitionMap(transitionMap);
        }
        struct Transition
        {
            public Action Action { get; set; }
            public State To { get; set; }
           public Transition(Action Action1, State To1)
            {
                Action = Action1;
                To = To1;
            }
        }
        public void AddWater()
        {
            boiler.waterLevel = 100;
            boiler.waterTemperature = 25;
        }
        public int ReadWaterLevel()
        {
            return boiler.waterLevel;
        }
        public int ReadWaterTemperature()
        {
            return boiler.waterTemperature;
        }
        public int SetWaterTemperature(int temperaturaDorita)
        {
            boiler.waterTemperature = temperaturaDorita;
            return boiler.waterTemperature;
        }
        public void AddCoffee(int coffeegramsAdaugate)
        {
            coffeegrams = coffeegrams + coffeegramsAdaugate;
        }
        public void RemoveCoffee()
        {
            coffeegrams = 0;
        }
        public int GetCoffeeMass()
        {
            return coffeegrams;
        }
        public int GetPotMass()
        {
            if (!IsPot()) return 0;
            return plate.potMass;
        }
        public int PotTemperature()
        {
            if (!IsPot()) return 0;
            return plate.potTemperature;
        }
        public void PreheatPot(int temperatura)
        {
            plate.potTemperature = temperatura;
        }
        public void EmptyPot()
        {
            plate.potMass = 0;
        }
        public bool IsPot()
        {
            return pot.potInside;
        }
        public void SetPressure(int pressureDorita)
        {
            if (pressureDorita >= 9 && pressureDorita <= 14)
                pressure = pressureDorita;
            else
            {
                Console.WriteLine("Va rugam sa introduceti o valoare intre 9 si 14");
                pressure = 0;
            }
        }
        public int ReadPressure()
        {
            return pressure;
        }
        public void AddPot()
        {
            pot.potInside=true;
        }
        public void RemovePot()
        {
            pot.potInside=false;
        }
        public override string ToString()
        {
            string mesaj;
            if (IsPot())
                mesaj = "Pot in position\n";
            else mesaj = "Pot not in position\n";
            mesaj += "Pot mass: " + GetPotMass().ToString() + " grams\n";
            mesaj += "Pot Temperature: " + PotTemperature().ToString() + " Celsius degrees\n";
            mesaj += "Level of Water: " + ReadWaterLevel().ToString() + "%\n";
            mesaj += "Temperature of Water: " + ReadWaterTemperature().ToString() + " Celsius degrees\n";
            mesaj += "Coffee Mass: " + GetCoffeeMass().ToString() + " grams\n";
            mesaj += "Pressure: " + ReadPressure().ToString() + " bar\n";

            return mesaj;
        }
        public void Start()
        {
            boiler.waterLevel -= 2;
            plate.potMass += 5;
        }

        public void Stop()
        {
            boiler.waterLevel -= 1;
            plate.potMass += 2;
        }

        public void New()
        {
            RemovePot();
            RemoveCoffee();
            EmptyPot();
        }

        public void PrintAvailableAction(State currentState)
        {
            for (int i=0; i < transitionMap[currentState].Count; i++)           
                Console.WriteLine(transitionMap[currentState][i].Action);
        }
        public State GetNextState(State currentState, string commandFromLine)
        {
            for (int i = 0; i < transitionMap[currentState].Count; i++)
            {
                if(commandFromLine == transitionMap[currentState][i].Action.ToString())
                {
                    currentState = transitionMap[currentState][i].To;
                    break;
                }
            }
            return currentState;
        }
        public bool IsTheCommandPossible(State currentState, string commandFromLine)
        {
            for (int i = 0; i < transitionMap[currentState].Count; i++)
            {
                if (commandFromLine == transitionMap[currentState][i].Action.ToString())
                {
                    return true;
                }
            }
            return false;
        }

        public void ExecuteCommand (string commandFromLine, ref State currentState)
        {
            if (commandFromLine == Action.AddCoffee.ToString()) AddCoffee(18);         
            if (commandFromLine == Action.HeatWater.ToString()) SetWaterTemperature(70);
            if (commandFromLine == Action.RemovePot.ToString()) RemovePot();
            if (commandFromLine == Action.AddWater.ToString()) AddWater();           
            if (commandFromLine == Action.RemoveCoffee.ToString()) RemoveCoffee();
            if (commandFromLine == Action.SetPressure.ToString()) SetPressure(10);
            if (commandFromLine == Action.PreheatPot.ToString()) PreheatPot(40);
            if (commandFromLine == Action.AddPot.ToString()) AddPot();
            if (commandFromLine == Action.Start.ToString()) Start();
            if (commandFromLine == Action.Continue.ToString()) Start();
            if (commandFromLine == Action.Stop.ToString()) Stop();
            if (commandFromLine == Action.New.ToString())
            {
                New();
                if(ReadWaterLevel()<20)
                {
                    SetPressure(0);
                    currentState = State.Initial_state;
                }
            }

        }

        void FillTransitionMap(Dictionary<State, List<Transition>> transitionMap)
        {

            //Initial_state
            List<Transition> listInitialState = new List<Transition>();
            listInitialState.Add(new Transition(Action.AddPot, State.Pot));
            listInitialState.Add(new Transition(Action.AddWater, State.Water));
            listInitialState.Add(new Transition(Action.AddCoffee, State.Coffee));
            transitionMap.Add(State.Initial_state, listInitialState);

            //Pot
            List<Transition> listPot = new List<Transition>();
            listPot.Add(new Transition(Action.AddWater, State.Pot_Water));
            listPot.Add(new Transition(Action.AddCoffee, State.Pot_Coffee));
            listPot.Add(new Transition(Action.RemovePot, State.Initial_state));
            listPot.Add(new Transition(Action.PreheatPot, State.Pot_preheated));
            transitionMap.Add(State.Pot, listPot);

            //Water
            List<Transition> listWater = new List<Transition>();
            listWater.Add(new Transition(Action.HeatWater, State.Water_warmed));
            listWater.Add(new Transition(Action.AddPot, State.Pot_Water));
            listWater.Add(new Transition(Action.AddCoffee, State.Water_Coffee));
            transitionMap.Add(State.Water, listWater);

            //Coffee
            List<Transition> listCoffee = new List<Transition>();
            listCoffee.Add(new Transition(Action.AddPot, State.Pot_Coffee));
            listCoffee.Add(new Transition(Action.AddWater, State.Water_Coffee));
            listCoffee.Add(new Transition(Action.RemoveCoffee, State.Initial_state));
            transitionMap.Add(State.Coffee, listCoffee);

            //Pot_preheated
            List<Transition> listPotPreheated = new List<Transition>();
            listPotPreheated.Add(new Transition(Action.AddWater, State.Pot_preheated_Water));
            listPotPreheated.Add(new Transition(Action.AddCoffee, State.Pot_preheated_Coffee));
            listPotPreheated.Add(new Transition(Action.RemovePot, State.Initial_state));
            transitionMap.Add(State.Pot_preheated, listPotPreheated);

            //Pot_Water
            List<Transition> listPotWater = new List<Transition>();
            listPotWater.Add(new Transition(Action.AddCoffee, State.Pot_Water_Coffee));
            listPotWater.Add(new Transition(Action.RemovePot, State.Water));
            listPotWater.Add(new Transition(Action.PreheatPot, State.Pot_preheated_Water));
            listPotWater.Add(new Transition(Action.HeatWater, State.Pot_Water_warmed));
            transitionMap.Add(State.Pot_Water, listPotWater);

            //Pot_Coffee
            List<Transition> listPotCoffee = new List<Transition>();
            listPotCoffee.Add(new Transition(Action.AddWater, State.Pot_Water_Coffee));
            listPotCoffee.Add(new Transition(Action.RemoveCoffee, State.Pot));
            listPotCoffee.Add(new Transition(Action.RemovePot, State.Coffee));
            listPotCoffee.Add(new Transition(Action.PreheatPot, State.Pot_preheated_Coffee));
            transitionMap.Add(State.Pot_Coffee, listPotCoffee);

            //Pot_preheated_Water
            List<Transition> listPotpreheatedWater = new List<Transition>();
            listPotpreheatedWater.Add(new Transition(Action.HeatWater, State.Pot_preheated_Water_warmed));
            listPotpreheatedWater.Add(new Transition(Action.AddCoffee, State.Pot_preheated_Water_Coffee));
            listPotpreheatedWater.Add(new Transition(Action.RemovePot, State.Water));
            transitionMap.Add(State.Pot_preheated_Water, listPotpreheatedWater);

            //Pot_preheated_Coffee
            List<Transition> listPotpreheatedCoffee = new List<Transition>();
            listPotpreheatedCoffee.Add(new Transition(Action.AddWater, State.Pot_preheated_Water_Coffee));
            listPotpreheatedCoffee.Add(new Transition(Action.RemoveCoffee, State.Pot_preheated));
            listPotpreheatedCoffee.Add(new Transition(Action.RemovePot, State.Coffee));
            transitionMap.Add(State.Pot_preheated_Coffee, listPotpreheatedCoffee);

            //Pot_preheated_Water_warmed
            List<Transition> listPotpreheatedWaterwarned = new List<Transition>();
            listPotpreheatedWaterwarned.Add(new Transition(Action.SetPressure, State.Pot_preheated_Water_warmed_Pressure));
            listPotpreheatedWaterwarned.Add(new Transition(Action.AddCoffee, State.Pot_preheated_Water_warmed_Coffee));
            listPotpreheatedWaterwarned.Add(new Transition(Action.RemovePot, State.Water_warmed));
            transitionMap.Add(State.Pot_preheated_Water_warmed, listPotpreheatedWaterwarned);

            //Pot_preheated_Water_Coffee
            List<Transition> listPotpreheatedWaterCoffee = new List<Transition>();
            listPotpreheatedWaterCoffee.Add(new Transition(Action.HeatWater, State.Pot_preheated_Water_warmed_Coffee));
            listPotpreheatedWaterCoffee.Add(new Transition(Action.RemoveCoffee, State.Pot_preheated_Water));
            listPotpreheatedWaterCoffee.Add(new Transition(Action.RemovePot, State.Water_Coffee));
            transitionMap.Add(State.Pot_preheated_Water_Coffee, listPotpreheatedWaterCoffee);

            //Pot_preheated_Water_warmed_Pressure
            List<Transition> listPotpreheatedWaterwarmedPressure = new List<Transition>();
            listPotpreheatedWaterwarmedPressure.Add(new Transition(Action.RemovePot, State.Water_warmed_Pressure));
            listPotpreheatedWaterwarmedPressure.Add(new Transition(Action.AddCoffee, State.Pot_preheated_Water_warmed_Coffee_Pressure));
            transitionMap.Add(State.Pot_preheated_Water_warmed_Pressure, listPotpreheatedWaterwarmedPressure);

            //Pot_preheated_Water_warmed_Coffee
            List<Transition> listPotpreheatedWaterwarmedCoffee = new List<Transition>();
            listPotpreheatedWaterwarmedCoffee.Add(new Transition(Action.RemoveCoffee, State.Pot_preheated_Water_warmed));
            listPotpreheatedWaterwarmedCoffee.Add(new Transition(Action.SetPressure, State.Pot_preheated_Water_warmed_Coffee_Pressure));
            listPotpreheatedWaterwarmedCoffee.Add(new Transition(Action.RemovePot, State.Water_warmed_Coffee));
            transitionMap.Add(State.Pot_preheated_Water_warmed_Coffee, listPotpreheatedWaterwarmedCoffee);

            //Pot_preheated_Water_warmed_Coffee_Pressure
            List<Transition> listPotpreheatedWaterwarmedCoffeePressure = new List<Transition>();
            listPotpreheatedWaterwarmedCoffeePressure.Add(new Transition(Action.Start, State.Filtering));
            listPotpreheatedWaterwarmedCoffeePressure.Add(new Transition(Action.RemoveCoffee, State.Pot_preheated_Water_warmed_Pressure));
            listPotpreheatedWaterwarmedCoffeePressure.Add(new Transition(Action.RemovePot, State.Water_warmed_Coffee_Pressure));
            transitionMap.Add(State.Pot_preheated_Water_warmed_Coffee_Pressure, listPotpreheatedWaterwarmedCoffeePressure);

            //Filtering
            List<Transition> listFiletring = new List<Transition>();
            listFiletring.Add(new Transition(Action.Stop, State.STOP));
            listFiletring.Add(new Transition(Action.Continue, State.Filtering));
            transitionMap.Add(State.Filtering, listFiletring);

            //STOP
            List<Transition> listStop = new List<Transition>();
            listStop.Add(new Transition(Action.Start, State.Filtering));
            listStop.Add(new Transition(Action.New, State.Water_warmed_Pressure));
            transitionMap.Add(State.STOP, listStop);

            //Pot_Water_warmed
            List<Transition> listPotWaterwarmed = new List<Transition>();
            listPotWaterwarmed.Add(new Transition(Action.AddCoffee, State.Pot_Water_warmed_Coffee));
            listPotWaterwarmed.Add(new Transition(Action.RemovePot, State.Water_warmed));
            listPotWaterwarmed.Add(new Transition(Action.PreheatPot, State.Pot_preheated_Water_warmed));
            listPotWaterwarmed.Add(new Transition(Action.SetPressure, State.Pot_Water_warmed_Pressure));
            transitionMap.Add(State.Pot_Water_warmed, listPotWaterwarmed);

            //Pot_Water_Coffee
            List<Transition> listPotWaterCoffee = new List<Transition>();
            listPotWaterCoffee.Add(new Transition(Action.RemoveCoffee, State.Pot_Water));
            listPotWaterCoffee.Add(new Transition(Action.RemovePot, State.Water_Coffee));
            listPotWaterCoffee.Add(new Transition(Action.PreheatPot, State.Pot_preheated_Water_Coffee));
            listPotWaterCoffee.Add(new Transition(Action.HeatWater, State.Pot_Water_warmed_Coffee));
            transitionMap.Add(State.Pot_Water_Coffee, listPotWaterCoffee);

            //Pot_Water_warmed_Pressure
            List<Transition> listPotWaterwarmedPressure = new List<Transition>();
            listPotWaterwarmedPressure.Add(new Transition(Action.PreheatPot, State.Pot_preheated_Water_warmed_Pressure));
            listPotWaterwarmedPressure.Add(new Transition(Action.AddCoffee, State.Pot_Water_warmed_Coffee_Pressure));
            listPotWaterwarmedPressure.Add(new Transition(Action.RemovePot, State.Water_warmed_Pressure));
            transitionMap.Add(State.Pot_Water_warmed_Pressure, listPotWaterwarmedPressure);

            //Pot_Water_warmed_Coffee
            List<Transition> listPotWaterwarmedCoffee = new List<Transition>();
            listPotWaterwarmedCoffee.Add(new Transition(Action.PreheatPot, State.Pot_preheated_Water_warmed_Coffee));
            listPotWaterwarmedCoffee.Add(new Transition(Action.RemoveCoffee, State.Pot_Water_warmed_Pressure));
            listPotWaterwarmedCoffee.Add(new Transition(Action.RemovePot, State.Water_warmed_Coffee));
            listPotWaterwarmedCoffee.Add(new Transition(Action.SetPressure, State.Pot_Water_warmed_Coffee_Pressure));
            transitionMap.Add(State.Pot_Water_warmed_Coffee, listPotWaterwarmedCoffee);

            //Pot_Water_warmed_Coffee_Pressure
            List<Transition> listPotWaterwarmedCoffeePressure = new List<Transition>();
            listPotWaterwarmedCoffeePressure.Add(new Transition(Action.PreheatPot, State.Pot_preheated_Water_warmed_Coffee_Pressure));
            listPotWaterwarmedCoffeePressure.Add(new Transition(Action.RemoveCoffee, State.Pot_Water_warmed_Pressure));
            listPotWaterwarmedCoffeePressure.Add(new Transition(Action.RemovePot, State.Water_warmed_Pressure));
            transitionMap.Add(State.Pot_Water_warmed_Coffee_Pressure, listPotWaterwarmedCoffeePressure);

            //Water_Coffee
            List<Transition> listWaterCoffee = new List<Transition>();
            listWaterCoffee.Add(new Transition(Action.AddPot, State.Pot_Water_Coffee));
            listWaterCoffee.Add(new Transition(Action.RemoveCoffee, State.Water));
            listWaterCoffee.Add(new Transition(Action.HeatWater, State.Water_warmed_Coffee));
            transitionMap.Add(State.Water_Coffee, listWaterCoffee);

            //Water_warmed
            List<Transition> listWaterwarmed = new List<Transition>();
            listWaterwarmed.Add(new Transition(Action.AddPot, State.Pot_Water_warmed));
            listWaterwarmed.Add(new Transition(Action.AddCoffee, State.Water_warmed_Coffee));
            listWaterwarmed.Add(new Transition(Action.SetPressure, State.Water_warmed_Pressure));
            transitionMap.Add(State.Water_warmed, listWaterwarmed);

            //Water_warmed_Coffee
            List<Transition> listWaterwarmedCoffee = new List<Transition>();
            listWaterwarmedCoffee.Add(new Transition(Action.AddPot, State.Pot_Water_warmed_Coffee));
            listWaterwarmedCoffee.Add(new Transition(Action.RemoveCoffee, State.Water_warmed));
            listWaterwarmedCoffee.Add(new Transition(Action.SetPressure, State.Water_warmed_Coffee_Pressure));
            transitionMap.Add(State.Water_warmed_Coffee, listWaterwarmedCoffee);

            //Water_warmed_Cofee_Pressure
            List<Transition> listWaterwarmedCoffeePressure = new List<Transition>();
            listWaterwarmedCoffeePressure.Add(new Transition(Action.AddPot, State.Pot_Water_warmed_Coffee_Pressure));
            listWaterwarmedCoffeePressure.Add(new Transition(Action.RemoveCoffee, State.Water_warmed_Pressure));
            transitionMap.Add(State.Water_warmed_Coffee_Pressure, listWaterwarmedCoffeePressure);

            //Water_warmed_Pressure
            List<Transition> listWaterwarmedPressure = new List<Transition>();
            listWaterwarmedPressure.Add(new Transition(Action.AddPot, State.Pot_Water_warmed_Pressure));
            listWaterwarmedPressure.Add(new Transition(Action.AddCoffee, State.Water_warmed_Coffee_Pressure));
            transitionMap.Add(State.Water_warmed_Pressure, listWaterwarmedPressure);
        }

    }
}