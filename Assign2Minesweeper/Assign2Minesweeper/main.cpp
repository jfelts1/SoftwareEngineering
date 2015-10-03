//StringUtils and FileUtils is from my game
//compiles using VS 2015 and g++-5
#include <cstdlib>
#include <cstring>
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
using std::cout;
using std::endl;

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
	in = Utils::readFileAsText("in.txt");
	//in = Utils::readInput();
	//cout << "read in successfully" << endl;
	vector<string> splitIn = Utils::splitString(in,'\n');
	//cout << "split successfully" << endl;
	std::vector<MineField> mineFields = MineField::getMineFields(splitIn);
	for(auto& field :mineFields)
	{
		cout<<field<<endl;
	}
	return EXIT_SUCCESS;
}
