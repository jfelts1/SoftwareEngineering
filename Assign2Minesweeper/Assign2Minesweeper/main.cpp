//StringUtils and FileUtils is from my game
#include <cstdlib>
#include <iostream>
#include <string>
#include <vector>
#include "Utils/StringUtils.h"
#include "Utils/FileUtils.h"
#include "Utils/ConsoleUtils.h"
#include "MineField.h"

using std::string;
using std::vector;

int main()
{
	string in;
	//in = Utils::readFileAsText("out.txt");
	in = Utils::readInput();
	vector<string> splitIn = Utils::splitString(in,'\n');
	
	for(auto& str: splitIn)
	{
		std::cout<<str<<std::endl;
	}
	return EXIT_SUCCESS;
}
