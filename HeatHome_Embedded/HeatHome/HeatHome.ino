/*
 Name:		HeatHome.ino
 Created:	10/5/2022 9:03:53 AM
 Author:	Zanxus
*/

/* Arduino example code for DHT11, DHT22/AM2302 and DHT21/AM2301 temperature and humidity sensors. More info: www.www.makerguides.com */

// Include the libraries:

//for it being a client
#include <stdio.h>
#include <ESP8266WiFi.h>
#include <WiFiClient.h>

//for websockets
#include <WebSocketsClient.h>

//for the sensor
#include <Adafruit_Sensor.h>
#include <DHT.h>


#pragma region Wifi Variables
WebSocketsClient webSocket;

const char* ssid = "h5pd091122";
const char* password = "h5pd091122_styrer";

const String IP = "192.168.1.100";
const int PORT = 45455;
#pragma endregion



#pragma region Sensor Variables
// Set DHT type, uncomment whatever type you're using!
#define DHTTYPE DHT22   // DHT 22  (AM2302)
// Initialize DHT sensor for normal 16mhz Arduino:
DHT dht = DHT(D3, DHTTYPE);

float lastTemp = 0;
#pragma endregion


void setup() {
    // Begin serial communication at a baud rate of 9600:
    Serial.begin(9600);

    pinMode(D6, OUTPUT);

    Serial.print("Connecting to ");
    Serial.println(ssid);
    WiFi.begin(ssid, password);
    while (WiFi.status() != WL_CONNECTED) {
        delay(500);
        Serial.print(".");
    }
    Serial.println("");
    Serial.print("Connected to WiFi network with IP Address: ");
    Serial.println(WiFi.localIP());

    // server address, port and URL
    webSocket.begin(IP, PORT, "/ws");

    // event handler
    webSocket.onEvent(webSocketEvent);

    // use HTTP Basic Authorization this is optional remove if not needed
    //webSocket.setAuthorization("user", "Password");

    // try ever 5000 again if connection has failed
    webSocket.setReconnectInterval(5000);

    // start heartbeat (optional)
    // ping server every 15000 ms
    // expect pong from server within 5000 ms
    // consider connection disconnected if pong is not received 2 times
    webSocket.enableHeartbeat(15000, 5000, 2);

    // Setup sensor:
    dht.begin();
}

void loop() {

    float t = handleTemperature();
    //float t = NULL;
    if (t != NULL)
    {
        handleWebsocket(t);
    }
    webSocket.loop();
}

/// <summary>
/// reads the temperature every 2 seconds and returns it, returns null if no messurement could be taken.
/// </summary>
/// <returns></returns>
float handleTemperature() {
    // Wait a few seconds between measurements:

    // Reading temperature or humidity takes about 250 milliseconds!
    // Sensor readings may also be up to 2 seconds 'old' (its a very slow sensor)

    // Read the humidity in %:
    float h = dht.readHumidity();
    // Read the temperature as Celsius:
    float t = dht.readTemperature();


    // Check if any reads failed and exit early (to try again):
    if (isnan(h) || isnan(t)) {
        Serial.println("Failed to read from DHT sensor!");
        return NULL;
    }

    // Compute heat index in Celsius:
    float hic = dht.computeHeatIndex(t, h, false);

    Serial.print("Humidity: " + String(h)+"% | ");
    Serial.print("Temperature: " + String(t) + "\xC2\xB0 C | ");
    Serial.print("Heat index: " + String(hic) + "\xC2\xB0 C " );
    Serial.println();

    return t;
}

void handleWebsocket(float t) {
    String t_s = String(t);
    if (t_s != NULL && webSocket.isConnected() && lastTemp != t)
    {
        lastTemp = t;
        webSocket.sendTXT(t_s);
    }
}

void webSocketEvent(WStype_t type, uint8_t* payload, size_t length) {

    Serial.println("Event was triggered");
    switch (type) {
    case WStype_DISCONNECTED:
        Serial.printf("[WSc] Disconnected!\n");
        break;
    case WStype_CONNECTED: {
        Serial.printf("[WSc] Connected to url: %s\n", payload);

        // send message to server when Connected
        String message = "Connected with macAddress: " + WiFi.macAddress();
        webSocket.sendTXT(message);
    }
        break;
    case WStype_TEXT:
        Serial.printf("[WSc] get text: %s\n", payload);

        // send message to server
        // webSocket.sendTXT("message here");
        break;
    case WStype_BIN:
        Serial.printf("[WSc] get binary length: %u\n", length);
        handleBinary(payload);
        // send data to server
        // webSocket.sendBIN(payload, length);
        break;
    case WStype_PING:
        // pong will be send automatically
        Serial.printf("[WSc] get ping\n");
        break;
    case WStype_PONG:
        // answer to a ping we send
        Serial.printf("[WSc] get pong\n");
        break;
    }

}


void handleBinary(uint8_t* payload) {
    
    uint8_t ShouldTurnOn = payload[0];
    if (ShouldTurnOn == 1)
    {
        Serial.println("Led should turn on.");
        digitalWrite(D6, HIGH);
    }
    else if (ShouldTurnOn == 0)
    {
        Serial.println("Led should turn off.");
        digitalWrite(D6, LOW);
    }
}

