#include "ConsoleUtils.h"

using std::string;
using std::ifstream;

std::string Utils::readInput()
{
	string rawString;
	string tmp;
	std::getline(std::cin, tmp);
	rawString.append(tmp);
	rawString.push_back('\n');
	while (!std::cin.eof())
	{
		std::getline(std::cin, tmp);
		rawString.append(tmp);
		rawString.push_back('\n');
	}

	//std::cout << rawString << std::endl;

	return rawString;
}
