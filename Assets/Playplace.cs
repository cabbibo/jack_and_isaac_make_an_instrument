// OSC Jack - Open Sound Control plugin for Unity
// https://github.com/keijiro/OscJack

using UnityEngine;
using OscJack;
using UnityEngine.Events;
using System.Collections.Generic;



[System.Serializable]
public class FloatEvent : UnityEvent<float>{}

[ExecuteAlways]
class Playplace : MonoBehaviour
{
    OscServer _server;

    public FloatEvent SelectEvent;



    public struct Event{
        public string address;
        public float data;

        public Event( string a , float d ){
            address = a;
            data = d;
        }
    }


    public List<Event> events;

    void OnEnable()
    {

        events = new List<Event>();


        _server = new OscServer(9000); // Port number

        _server.MessageDispatcher.AddCallback(
            "/sliders/1", // OSC address
            (string address, OscDataHandle data) => {
                Debug.Log(string.Format("({0})",
                    data.GetElementAsFloat(0)));
            }
        );

        _server.MessageDispatcher.AddCallback(
            "", // OSC address
            (string address, OscDataHandle data) => {
                events.Add( new Event( address, data.GetElementAsFloat(0)));
            } 
        );

    }


    public void Update(){

        foreach( Event e in events ){
            if( e.address == "/select"){
                OnSelect( e.data );
            }
        }

        events.Clear();

    }

    public void OnSelect(float v){
        SelectEvent.Invoke(v);
    }

    void OnDisable()
    {
        _server?.Dispose();
        _server = null;
    }
}
