using PositioningServer.Common.Data;
using PositioningServer.Common.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositioningServer.DBHandler
{
    class PrototypeDatabase
    {
        public PrototypeDatabase()
        {
            SetupFacade facade = SetupFacade.Instance;
            using(var reader = new StreamReader(@"C:\Users\mega-\Source\Repos\madsmogensen\PositioningServer\PositioningServer\DBHandler\uwb_GoCart.csv"))
            {
                if (!reader.EndOfStream) { reader.ReadLine(); } //skip header
                ISetup setup = facade.getSetup("From File");
                
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    string id = values[0];
                    int x = Int32.Parse(values[1]);
                    int y = Int32.Parse(values[2]);
                    int z = Int32.Parse(values[3]);
                    string[] date = values[4].Split(' ')[0].Split('-');
                    int year = Int32.Parse(date[0]);
                    int month = Int32.Parse(date[1]);
                    int day = Int32.Parse(date[2]);
                    string[] time = values[4].Split(' ')[1].Split(':');
                    int hour = Int32.Parse(time[0]);
                    int minute = Int32.Parse(time[1]);
                    int second = Int32.Parse(time[2].Split('.')[0]);
                    int millisecond = Int32.Parse(time[2].Split('.')[1]);
                    //public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond); // use .Ticks for long
                    DateTime dateTime = new DateTime(year,month,day,hour,minute,second,millisecond);
                    IUnit newNode = facade.makeUnit(id);
                    Coordinate coordinate = new Coordinate(x, y, z, dateTime);

                    newNode.coordinate(coordinate);
                    
                    setup.addRawNode(newNode);
                    
                }
            }
        }
    }
}
