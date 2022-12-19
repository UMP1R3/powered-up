using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using SharpBrick.PoweredUp;

namespace Example;

public class ExamplePavel : BaseExample
{
    public override async Task ExecuteAsync()
    {
        using var hub = Host.FindByType<TwoPortHub>();

        await hub.RgbLight.SetRgbColorsAsync(0x00, 0x00, 0xff);
        //await technicMediumHub.cc
        var volt = hub.Voltage;
        
        var battery = hub.BatteryVoltageInPercent;
        Console.WriteLine($"Jedeme na {battery}%");

        var motor = hub.A.GetDevice<SystemTrainMotor>();

        //for (int i = 0; i < 256; i+=32)
        //{
        //    for(int j = 0; j < 256; j+=32)
        //    {
        //        for (int k = 0; k < 256; k+=32)
        //        {
        //            Console.Write($"\r{i}; {j}; {k}     ");
        //            await hub.RgbLight.SetRgbColorsAsync(
        //                (byte)i,
        //                (byte)j,
        //                (byte)k
        //            );
        //            await Task.Delay(1);

        //        }
        //    }
        //}

        sbyte rychlost = 50;
        await motor.StartPowerAsync(10);
        await Task.Delay(1000);

        await motor.StartPowerAsync(20);
        await Task.Delay(1000);

        await motor.StartPowerAsync(30);
        await Task.Delay(1000);        
        

        Console.WriteLine("Zmacni \"ENTER\" pro konec");

        var run = true;

        while (run)
        {
            await motor.StartPowerAsync(rychlost);
            var key = Console.ReadKey();
            if(key.Key == ConsoleKey.Enter)
            {
                run = false;
            }
            else if (key.Key == ConsoleKey.LeftArrow && rychlost > 0)
            {
                rychlost -= 10;                
            }
            else if (key.Key == ConsoleKey.RightArrow && rychlost < 90)
            {
                rychlost += 10;
            }

            await Task.Delay(10);
        }


        Console.ReadKey();

        ////await motor.StopByBrakeAsync();
        //await motor.StopByFloatAsync();
        //await Task.Delay(5000);

        //await motor.StartPowerAsync(-25);
        //await Task.Delay(2000);




        await hub.SwitchOffAsync();
    }
}
