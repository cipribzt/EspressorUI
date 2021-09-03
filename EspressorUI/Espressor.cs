using System;
using System.Collections.Generic;


namespace EspressorUI
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

    public class Espressor
    {
        private Pot _pot = new Pot { potInside = false };
        private Plate _plate = new Plate
        {
            potMass = 0,
            potTemperature = 0
        };
        private Boiler _boiler = new Boiler
        {
            WaterLevel = 0,
            WaterTemperature = 0
        };
        private string _indicatorLight = "Green";
        public int CoffeeGrams { get; set; }
        public int Pressure { get; set; }
        private Dictionary<State, List<Transition>> _transitionMap;
        public Espressor()
        {
            _transitionMap = new Dictionary<State, List<Transition>>();
            _fillTransitionMap(_transitionMap);
            CoffeeGrams = 0;
            Pressure = 1;
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
            _boiler.WaterLevel = 100;
            _boiler.WaterTemperature = 25;
        }

        public int SetWaterTemperature(int newTemperature)
        {
            if (newTemperature <= 90 && newTemperature >= 60)
                _boiler.WaterTemperature = newTemperature;
            else
            {
                _boiler.WaterTemperature = 20;
                Console.WriteLine("Please enter a value between 60 and 90! ");
            }
            return _boiler.WaterTemperature;
        }

        public void AddCoffee(int newCoffeeGrams)
        {
            CoffeeGrams = newCoffeeGrams;
        }

        public void RemoveCoffee()
        {
            CoffeeGrams = 0;
        }

        public int GetPotMass()
        {
            if (!_pot.potInside) return 0;
            return _plate.potMass;
        }

        public int GetPotTemperature()
        {
            if (_pot.potInside) return 0;
            return _plate.potTemperature;
        }

        public void PreheatPot(int temperatura)
        {
            _plate.potTemperature = temperatura;
        }

        public void EmptyPot()
        {
            _plate.potMass = 0;
        }

        public void SetPressure(int newPressure)
        {
            if (newPressure >= 8 && newPressure <= 11)
                Pressure = newPressure;
            else
            {
                Console.WriteLine("Please enter a value between 8 and 11");
                Pressure = 0;
            }
        }

        public void AddPot()
        {
            _pot.potInside = true;
        }

        public void RemovePot()
        {
            _pot.potInside = false;
        }

        public override string ToString()
        {
            string mesaj;
            if (_pot.potInside)
                mesaj = "Pot in position\n";
            else mesaj = "Pot not in position\n";
            mesaj += "Pot mass: " + GetPotMass().ToString() + " grams\n";
            mesaj += "Pot Temperature: " + GetPotTemperature().ToString() + " Celsius degrees\n";
            mesaj += "Level of Water: " + _boiler.WaterLevel.ToString() + "%\n";
            mesaj += "Temperature of Water: " + _boiler.WaterTemperature.ToString() + " Celsius degrees\n";
            mesaj += "Coffee Mass: " + CoffeeGrams.ToString() + " grams\n";
            mesaj += "Pressure: " + Pressure.ToString() + " bar\n";
            mesaj += "Indicator Light: " + _indicatorLight;

            return mesaj;
        }

        public void Start()
        {
            _boiler.WaterLevel -= 2;
            _plate.potMass += 10;
            _indicatorLight = "Red";
        }

        public void Stop()
        {
            _boiler.WaterLevel -= 1;
            _plate.potMass += 2;
            _indicatorLight = "Green";
        }

        public void New()
        {
            RemovePot();
            RemoveCoffee();
            EmptyPot();
        }

        public void PrintAvailableAction(State currentState)
        {
            for (int i = 0; i < _transitionMap[currentState].Count; i++)
            {
                Console.WriteLine(_transitionMap[currentState][i].Action);
            }
        }

        public State GetNextState(State currentState, string commandFromLine)
        {
            for (int i = 0; i < _transitionMap[currentState].Count; i++)
            {
                if (commandFromLine == _transitionMap[currentState][i].Action.ToString())
                {
                    currentState = _transitionMap[currentState][i].To;
                    break;
                }
            }
            return currentState;
        }

        public bool IsTheCommandPossible(State currentState, string commandFromLine)
        {
            for (int i = 0; i < _transitionMap[currentState].Count; i++)
            {
                if (commandFromLine == _transitionMap[currentState][i].Action.ToString())
                {
                    return true;
                }
            }
            return false;
        }

        public void ExecuteCommand(string commandFromLine, ref State currentState)
        {
            string message = "Enter a value: ";
            string valueFromCommand;
            if (commandFromLine == Action.AddCoffee.ToString())
            {
                Console.Write(message);
                valueFromCommand = Console.ReadLine();
                AddCoffee(Convert.ToInt32(valueFromCommand));
            }
            if (commandFromLine == Action.HeatWater.ToString())
            {
                do
                {
                    Console.Write(message);
                    valueFromCommand = Console.ReadLine();
                    SetWaterTemperature(Convert.ToInt32(valueFromCommand));
                } while (_boiler.WaterTemperature == 20);
            }
            if (commandFromLine == Action.RemovePot.ToString()) RemovePot();
            if (commandFromLine == Action.AddWater.ToString()) AddWater();
            if (commandFromLine == Action.RemoveCoffee.ToString()) RemoveCoffee();
            if (commandFromLine == Action.SetPressure.ToString())
            {
                do
                {
                    Console.Write(message);
                    valueFromCommand = Console.ReadLine();
                    SetPressure(Convert.ToInt32(valueFromCommand));
                } while (Pressure == 0);
            }
            if (commandFromLine == Action.PreheatPot.ToString()) PreheatPot(40);
            if (commandFromLine == Action.AddPot.ToString()) AddPot();
            if (commandFromLine == Action.Start.ToString()) Start();
            if (commandFromLine == Action.Continue.ToString()) Start();
            if (commandFromLine == Action.Stop.ToString()) Stop();
            if (commandFromLine == Action.New.ToString())
            {
                New();
                if (_boiler.WaterLevel < 20)
                {
                    SetPressure(0);
                    currentState = State.Initial_state;
                }
            }

        }

        private void _fillTransitionMap(Dictionary<State, List<Transition>> _transitionMap)
        {

            //Initial_state
            List<Transition> listInitialState = new List<Transition>();
            listInitialState.Add(new Transition(Action.AddPot, State.Pot));
            listInitialState.Add(new Transition(Action.AddWater, State.Water));
            listInitialState.Add(new Transition(Action.AddCoffee, State.Coffee));
            _transitionMap.Add(State.Initial_state, listInitialState);

            //Pot
            List<Transition> listPot = new List<Transition>();
            listPot.Add(new Transition(Action.AddWater, State.Pot_Water));
            listPot.Add(new Transition(Action.AddCoffee, State.Pot_Coffee));
            listPot.Add(new Transition(Action.RemovePot, State.Initial_state));
            listPot.Add(new Transition(Action.PreheatPot, State.Pot_preheated));
            _transitionMap.Add(State.Pot, listPot);

            //Water
            List<Transition> listWater = new List<Transition>();
            listWater.Add(new Transition(Action.HeatWater, State.Water_warmed));
            listWater.Add(new Transition(Action.AddPot, State.Pot_Water));
            listWater.Add(new Transition(Action.AddCoffee, State.Water_Coffee));
            _transitionMap.Add(State.Water, listWater);

            //Coffee
            List<Transition> listCoffee = new List<Transition>();
            listCoffee.Add(new Transition(Action.AddPot, State.Pot_Coffee));
            listCoffee.Add(new Transition(Action.AddWater, State.Water_Coffee));
            listCoffee.Add(new Transition(Action.RemoveCoffee, State.Initial_state));
            _transitionMap.Add(State.Coffee, listCoffee);

            //Pot_preheated
            List<Transition> listPotPreheated = new List<Transition>();
            listPotPreheated.Add(new Transition(Action.AddWater, State.Pot_preheated_Water));
            listPotPreheated.Add(new Transition(Action.AddCoffee, State.Pot_preheated_Coffee));
            listPotPreheated.Add(new Transition(Action.RemovePot, State.Initial_state));
            _transitionMap.Add(State.Pot_preheated, listPotPreheated);

            //Pot_Water
            List<Transition> listPotWater = new List<Transition>();
            listPotWater.Add(new Transition(Action.AddCoffee, State.Pot_Water_Coffee));
            listPotWater.Add(new Transition(Action.RemovePot, State.Water));
            listPotWater.Add(new Transition(Action.PreheatPot, State.Pot_preheated_Water));
            listPotWater.Add(new Transition(Action.HeatWater, State.Pot_Water_warmed));
            _transitionMap.Add(State.Pot_Water, listPotWater);

            //Pot_Coffee
            List<Transition> listPotCoffee = new List<Transition>();
            listPotCoffee.Add(new Transition(Action.AddWater, State.Pot_Water_Coffee));
            listPotCoffee.Add(new Transition(Action.RemoveCoffee, State.Pot));
            listPotCoffee.Add(new Transition(Action.RemovePot, State.Coffee));
            listPotCoffee.Add(new Transition(Action.PreheatPot, State.Pot_preheated_Coffee));
            _transitionMap.Add(State.Pot_Coffee, listPotCoffee);

            //Pot_preheated_Water
            List<Transition> listPotpreheatedWater = new List<Transition>();
            listPotpreheatedWater.Add(new Transition(Action.HeatWater, State.Pot_preheated_Water_warmed));
            listPotpreheatedWater.Add(new Transition(Action.AddCoffee, State.Pot_preheated_Water_Coffee));
            listPotpreheatedWater.Add(new Transition(Action.RemovePot, State.Water));
            _transitionMap.Add(State.Pot_preheated_Water, listPotpreheatedWater);

            //Pot_preheated_Coffee
            List<Transition> listPotpreheatedCoffee = new List<Transition>();
            listPotpreheatedCoffee.Add(new Transition(Action.AddWater, State.Pot_preheated_Water_Coffee));
            listPotpreheatedCoffee.Add(new Transition(Action.RemoveCoffee, State.Pot_preheated));
            listPotpreheatedCoffee.Add(new Transition(Action.RemovePot, State.Coffee));
            _transitionMap.Add(State.Pot_preheated_Coffee, listPotpreheatedCoffee);

            //Pot_preheated_Water_warmed
            List<Transition> listPotpreheatedWaterwarned = new List<Transition>();
            listPotpreheatedWaterwarned.Add(new Transition(Action.SetPressure, State.Pot_preheated_Water_warmed_Pressure));
            listPotpreheatedWaterwarned.Add(new Transition(Action.AddCoffee, State.Pot_preheated_Water_warmed_Coffee));
            listPotpreheatedWaterwarned.Add(new Transition(Action.RemovePot, State.Water_warmed));
            _transitionMap.Add(State.Pot_preheated_Water_warmed, listPotpreheatedWaterwarned);

            //Pot_preheated_Water_Coffee
            List<Transition> listPotpreheatedWaterCoffee = new List<Transition>();
            listPotpreheatedWaterCoffee.Add(new Transition(Action.HeatWater, State.Pot_preheated_Water_warmed_Coffee));
            listPotpreheatedWaterCoffee.Add(new Transition(Action.RemoveCoffee, State.Pot_preheated_Water));
            listPotpreheatedWaterCoffee.Add(new Transition(Action.RemovePot, State.Water_Coffee));
            _transitionMap.Add(State.Pot_preheated_Water_Coffee, listPotpreheatedWaterCoffee);

            //Pot_preheated_Water_warmed_Pressure
            List<Transition> listPotpreheatedWaterwarmedPressure = new List<Transition>();
            listPotpreheatedWaterwarmedPressure.Add(new Transition(Action.RemovePot, State.Water_warmed_Pressure));
            listPotpreheatedWaterwarmedPressure.Add(new Transition(Action.AddCoffee, State.Pot_preheated_Water_warmed_Coffee_Pressure));
            _transitionMap.Add(State.Pot_preheated_Water_warmed_Pressure, listPotpreheatedWaterwarmedPressure);

            //Pot_preheated_Water_warmed_Coffee
            List<Transition> listPotpreheatedWaterwarmedCoffee = new List<Transition>();
            listPotpreheatedWaterwarmedCoffee.Add(new Transition(Action.RemoveCoffee, State.Pot_preheated_Water_warmed));
            listPotpreheatedWaterwarmedCoffee.Add(new Transition(Action.SetPressure, State.Pot_preheated_Water_warmed_Coffee_Pressure));
            listPotpreheatedWaterwarmedCoffee.Add(new Transition(Action.RemovePot, State.Water_warmed_Coffee));
            _transitionMap.Add(State.Pot_preheated_Water_warmed_Coffee, listPotpreheatedWaterwarmedCoffee);

            //Pot_preheated_Water_warmed_Coffee_Pressure
            List<Transition> listPotpreheatedWaterwarmedCoffeePressure = new List<Transition>();
            listPotpreheatedWaterwarmedCoffeePressure.Add(new Transition(Action.Start, State.Filtering));
            listPotpreheatedWaterwarmedCoffeePressure.Add(new Transition(Action.RemoveCoffee, State.Pot_preheated_Water_warmed_Pressure));
            listPotpreheatedWaterwarmedCoffeePressure.Add(new Transition(Action.RemovePot, State.Water_warmed_Coffee_Pressure));
            _transitionMap.Add(State.Pot_preheated_Water_warmed_Coffee_Pressure, listPotpreheatedWaterwarmedCoffeePressure);

            //Filtering
            List<Transition> listFiletring = new List<Transition>();
            listFiletring.Add(new Transition(Action.Stop, State.STOP));
            listFiletring.Add(new Transition(Action.Continue, State.Filtering));
            _transitionMap.Add(State.Filtering, listFiletring);

            //STOP
            List<Transition> listStop = new List<Transition>();
            listStop.Add(new Transition(Action.Start, State.Filtering));
            listStop.Add(new Transition(Action.New, State.Water_warmed_Pressure));
            _transitionMap.Add(State.STOP, listStop);

            //Pot_Water_warmed
            List<Transition> listPotWaterwarmed = new List<Transition>();
            listPotWaterwarmed.Add(new Transition(Action.AddCoffee, State.Pot_Water_warmed_Coffee));
            listPotWaterwarmed.Add(new Transition(Action.RemovePot, State.Water_warmed));
            listPotWaterwarmed.Add(new Transition(Action.PreheatPot, State.Pot_preheated_Water_warmed));
            listPotWaterwarmed.Add(new Transition(Action.SetPressure, State.Pot_Water_warmed_Pressure));
            _transitionMap.Add(State.Pot_Water_warmed, listPotWaterwarmed);

            //Pot_Water_Coffee
            List<Transition> listPotWaterCoffee = new List<Transition>();
            listPotWaterCoffee.Add(new Transition(Action.RemoveCoffee, State.Pot_Water));
            listPotWaterCoffee.Add(new Transition(Action.RemovePot, State.Water_Coffee));
            listPotWaterCoffee.Add(new Transition(Action.PreheatPot, State.Pot_preheated_Water_Coffee));
            listPotWaterCoffee.Add(new Transition(Action.HeatWater, State.Pot_Water_warmed_Coffee));
            _transitionMap.Add(State.Pot_Water_Coffee, listPotWaterCoffee);

            //Pot_Water_warmed_Pressure
            List<Transition> listPotWaterwarmedPressure = new List<Transition>();
            listPotWaterwarmedPressure.Add(new Transition(Action.PreheatPot, State.Pot_preheated_Water_warmed_Pressure));
            listPotWaterwarmedPressure.Add(new Transition(Action.AddCoffee, State.Pot_Water_warmed_Coffee_Pressure));
            listPotWaterwarmedPressure.Add(new Transition(Action.RemovePot, State.Water_warmed_Pressure));
            _transitionMap.Add(State.Pot_Water_warmed_Pressure, listPotWaterwarmedPressure);

            //Pot_Water_warmed_Coffee
            List<Transition> listPotWaterwarmedCoffee = new List<Transition>();
            listPotWaterwarmedCoffee.Add(new Transition(Action.PreheatPot, State.Pot_preheated_Water_warmed_Coffee));
            listPotWaterwarmedCoffee.Add(new Transition(Action.RemoveCoffee, State.Pot_Water_warmed_Pressure));
            listPotWaterwarmedCoffee.Add(new Transition(Action.RemovePot, State.Water_warmed_Coffee));
            listPotWaterwarmedCoffee.Add(new Transition(Action.SetPressure, State.Pot_Water_warmed_Coffee_Pressure));
            _transitionMap.Add(State.Pot_Water_warmed_Coffee, listPotWaterwarmedCoffee);

            //Pot_Water_warmed_Coffee_Pressure
            List<Transition> listPotWaterwarmedCoffeePressure = new List<Transition>();
            listPotWaterwarmedCoffeePressure.Add(new Transition(Action.PreheatPot, State.Pot_preheated_Water_warmed_Coffee_Pressure));
            listPotWaterwarmedCoffeePressure.Add(new Transition(Action.RemoveCoffee, State.Pot_Water_warmed_Pressure));
            listPotWaterwarmedCoffeePressure.Add(new Transition(Action.RemovePot, State.Water_warmed_Pressure));
            _transitionMap.Add(State.Pot_Water_warmed_Coffee_Pressure, listPotWaterwarmedCoffeePressure);

            //Water_Coffee
            List<Transition> listWaterCoffee = new List<Transition>();
            listWaterCoffee.Add(new Transition(Action.AddPot, State.Pot_Water_Coffee));
            listWaterCoffee.Add(new Transition(Action.RemoveCoffee, State.Water));
            listWaterCoffee.Add(new Transition(Action.HeatWater, State.Water_warmed_Coffee));
            _transitionMap.Add(State.Water_Coffee, listWaterCoffee);

            //Water_warmed
            List<Transition> listWaterwarmed = new List<Transition>();
            listWaterwarmed.Add(new Transition(Action.AddPot, State.Pot_Water_warmed));
            listWaterwarmed.Add(new Transition(Action.AddCoffee, State.Water_warmed_Coffee));
            listWaterwarmed.Add(new Transition(Action.SetPressure, State.Water_warmed_Pressure));
            _transitionMap.Add(State.Water_warmed, listWaterwarmed);

            //Water_warmed_Coffee
            List<Transition> listWaterwarmedCoffee = new List<Transition>();
            listWaterwarmedCoffee.Add(new Transition(Action.AddPot, State.Pot_Water_warmed_Coffee));
            listWaterwarmedCoffee.Add(new Transition(Action.RemoveCoffee, State.Water_warmed));
            listWaterwarmedCoffee.Add(new Transition(Action.SetPressure, State.Water_warmed_Coffee_Pressure));
            _transitionMap.Add(State.Water_warmed_Coffee, listWaterwarmedCoffee);

            //Water_warmed_Cofee_Pressure
            List<Transition> listWaterwarmedCoffeePressure = new List<Transition>();
            listWaterwarmedCoffeePressure.Add(new Transition(Action.AddPot, State.Pot_Water_warmed_Coffee_Pressure));
            listWaterwarmedCoffeePressure.Add(new Transition(Action.RemoveCoffee, State.Water_warmed_Pressure));
            _transitionMap.Add(State.Water_warmed_Coffee_Pressure, listWaterwarmedCoffeePressure);

            //Water_warmed_Pressure
            List<Transition> listWaterwarmedPressure = new List<Transition>();
            listWaterwarmedPressure.Add(new Transition(Action.AddPot, State.Pot_Water_warmed_Pressure));
            listWaterwarmedPressure.Add(new Transition(Action.AddCoffee, State.Water_warmed_Coffee_Pressure));
            _transitionMap.Add(State.Water_warmed_Pressure, listWaterwarmedPressure);
        }

    }
}