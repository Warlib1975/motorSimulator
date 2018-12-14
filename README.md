# Electrical Motor Simulator IoT Edge module
Microsoft Azure IoT Edge/Hub module for simulation an electric motor telemetry. 
Simulates parameters: current, voltage, torque, frequency, power, speed, energy, ambient temperature and humidity. 
It's based on Microsoft Azure IoT Edge simulated temperature sensor C# sources. 
https://github.com/Azure/iot-edge-v1/tree/master/v2/samples/azureiotedge-simulated-temperature-sensor

Some initial description is here (in Russian): http://www.bizkit.ru/2018/11/12/5737/

To build module you need to do some changes in files:
1. Create in Microsoft Azure IoT Hub new Continers Registry. Get username and password to push compiled module. 
2. Create file with name ".env" filled with created Container Registry authorization data:   
	CONTAINER_REGISTRY_USERNAME_docker=
	CONTAINER_REGISTRY_PASSWORD_docker=
3. Change the property "repository" in the file module.json for right repository URI: 
	"repository": "warlibregistry.azurecr.io/iot-edge-engine-simulator",
 
