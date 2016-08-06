// app.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <string>

extern "C" void publishMessageToMQ(char*);
extern "C" void openConnection();
extern "C" void closeConnection();

int main()
{
	openConnection();

	publishMessageToMQ("Hello from C++");
	publishMessageToMQ("Now i'm closing");

	closeConnection();

	return 0;
}

