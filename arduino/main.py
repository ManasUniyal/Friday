import sys
if len(sys.argv) <= 1:
    print("Not Enough Arguments")
    exit()
string = """#include <SoftwareSerial.h>
void(* resetFunc) (void) = 0;
SoftwareSerial mySerial(9, 10); // RX, TX
void setup()
{
  mySerial.begin(9600);               // the GPRS baud rate   
  Serial.begin(9600);               // the GPRS baud rate   
  delay(2000);
}

void loop()
{
  String number;
  int count=0;
 if(Serial.available()>0)
 {
  if(Serial.readString()=="call")
  {
    // resetFunc(); 
  }
   else if(Serial.available()>0)
 {
  number=Serial.readString();
 }
 Serial.println(number);
  mySerial.println("ATD9057261430;"); // xxxxxxxxx is the number you want to dial, Noice the ";" in the end
  delay(2000);
     while(1)
     {
       mySerial.println("AT+SPWM=2,63,100");// set PWM 2 PIN
       delay(100); 
       mySerial.println("AT+SPWM=1,63,100");
       delay(100);       
       mySerial.println("AT+SGPIO=0,1,1,1");// set GPIO 1 PIN to 1
       delay(100);
       mySerial.println("AT+SGPIO=0,2,1,1");
       delay(100);
       mySerial.println("AT+SGPIO=0,3,1,1");
       delay(100);
       mySerial.println("AT+SGPIO=0,4,1,1");
       delay(100);
       mySerial.println("AT+SGPIO=0,5,1,1");
       delay(100);
       mySerial.println("AT+SGPIO=0,6,1,1");
       delay(100);
       mySerial.println("AT+SGPIO=0,7,1,1");
       delay(100);
       mySerial.println("AT+SGPIO=0,8,1,1");
       delay(100);
       mySerial.println("AT+SGPIO=0,9,1,1");
       delay(100);
       mySerial.println("AT+SGPIO=0,10,1,1");
       delay(100);       
       mySerial.println("AT+SGPIO=0,11,1,1");
       delay(100);
       mySerial.println("AT+SGPIO=0,12,1,1");

       delay(500);

       mySerial.println("AT+SPWM=1,63,0");
       delay(100); 
       mySerial.println("AT+SPWM=2,63,0");
       delay(100);
       mySerial.println("AT+SGPIO=0,1,1,0"); // set GPIO 1 PIN to 0
       delay(100);
       mySerial.println("AT+SGPIO=0,2,1,0");
       delay(100);
       mySerial.println("AT+SGPIO=0,3,1,0");
       delay(100);
       mySerial.println("AT+SGPIO=0,4,1,0");
       delay(100);
       mySerial.println("AT+SGPIO=0,5,1,0");
       delay(100);
       mySerial.println("AT+SGPIO=0,6,1,0");
       delay(100);
       mySerial.println("AT+SGPIO=0,7,1,0");
       delay(100);
       mySerial.println("AT+SGPIO=0,8,1,0");
       delay(100);
       mySerial.println("AT+SGPIO=0,9,1,0");
       delay(100);
       mySerial.println("AT+SGPIO=0,10,1,0");
       delay(100);       
       mySerial.println("AT+SGPIO=0,11,1,0");
       delay(100);
       mySerial.println("AT+SGPIO=0,12,1,0");
       delay(500);

       count++;

       if(count==25)
       {
         mySerial.println("ATH"); //end the call.
         if(mySerial.available())
        {
           Serial.print((unsigned char)mySerial.read());

         } 
       } 
     } }}"""

string = string.replace('9057261430',sys.argv[1],1)
f = open("program.nso","w")
f.write(string)