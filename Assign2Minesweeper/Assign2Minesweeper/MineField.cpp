#include "MineField.h"

using std::vector;
using std::string;
using std::stringstream;

MineField::~MineField()
{}

std::vector<MineField> MineField::getMineFields(const std::vector<std::string> stringVec)
{
	vector<MineField> ret;
	vector<vector<byte>> fieldData;
	int x, y;
	stringstream tmp;
	int count = 0;
	//not using range for so I can exit the loop early
	for (auto begin = stringVec.begin(), end = stringVec.end();begin != end;++begin)
	{
		x = y = 0;
		tmp = stringstream(*begin);
		tmp >> x;
		tmp >> y;

		//exit loop when x and y are both 0
		if (x == y == 0)
		{
			begin = end;
		}
	}

	return ret;
}
