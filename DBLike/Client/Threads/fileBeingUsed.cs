using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Client.Threads
{
    class fileBeingUsed
    {

        public class eventDetails : IEquatable<eventDetails>
        {
            public string filepath;
            public DateTime datetime;
            public string eventType;
            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;
                eventDetails objAsDet = obj as eventDetails;
                if (objAsDet == null)
                    return false;
                else
                {
                    if (objAsDet.datetime == this.datetime
                        && objAsDet.eventType == this.eventType
                        && (objAsDet.filepath == this.filepath))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                //return Equals(objAsPart);
            }

            public bool Equals(eventDetails other)
            {
                if (other == null) return false;
                else
                {
                    if (other.datetime == this.datetime
                            && other.eventType == this.eventType
                            && (other.filepath == this.filepath))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        //static Dictionary<eventDetails, string> listOfFile = new Dictionary<eventDetails, string>();
        static List<eventDetails> listOfFile = new List<eventDetails>();
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addToList(eventDetails e)
        {
            listOfFile.Add(e);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void removefromList(eventDetails e)
        {
            listOfFile.Remove(e);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool alreadyPresent(eventDetails e)
        {
            if (listOfFile.Contains(e))
            {
                return true;
            }
            else
            {
                if(e.eventType.Equals("change"))
                {
                    eventDetails ev = new eventDetails();
                    ev.datetime = e.datetime;
                    ev.eventType = "create";
                    ev.filepath = e.filepath;
                    if(listOfFile.Contains(ev))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

    }
}
