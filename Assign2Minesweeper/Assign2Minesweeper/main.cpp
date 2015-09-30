//StringUtils and FileUtils is from my game
#include <cstdlib>
#include <iostream>
#include <string>
#include <vector>
#include "Utils/StringUtils.h"
#include "Utils/FileUtils.h"
#include "Utils/ConsoleUtils.h"
#include "MineField.h"
#define CONSOLE_BUFFER_SIZE 10000

using std::string;
using std::vector;

int main()
{
	int check = 0;
	//cuts the output time by a factor of 10 or so on windows!
	check = setvbuf(stdout, NULL, _IOFBF, CONSOLE_BUFFER_SIZE);
	if (check != 0)
	{
		std::cerr << "Unable to set buffer size" << std::endl;
	}
	string in;
	//in = Utils::readFileAsText("out.txt");
	in = Utils::readInput();
	vector<string> splitIn = Utils::splitString(in,'\n');
	std::vector<MineField> mineFields = MineField::getMineFields(splitIn);
	for(auto& str: splitIn)
	{
		std::cout<<str<<std::endl;
	}
	return EXIT_SUCCESS;
}
