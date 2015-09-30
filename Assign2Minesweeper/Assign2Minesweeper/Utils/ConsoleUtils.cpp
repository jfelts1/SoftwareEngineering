#include "ConsoleUtils.h"

using std::string;
using std::ifstream;
using std::stringstream;

std::string Utils::readInput()
{
	string rawString;
	string tmp;
	std::getline(std::cin, tmp);
	rawString.append(tmp);
	rawString.push_back('\n');
	int x,y;
	stringstream stream = stringstream(tmp);
	stream >> x;
	stream >> y;
	while (!(x==0 && y==0))
	{
		std::getline(std::cin, tmp);
		rawString.append(tmp);
		stream = stringstream(tmp);
		stream >> x;
		stream >> y;
		rawString.push_back('\n');
	}
	
	//std::cout << rawString << std::endl;

	return rawString;
}
