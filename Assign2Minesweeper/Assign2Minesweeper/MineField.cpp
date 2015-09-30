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
	
	int count = 0;
	std::vector<std::vector<std::string>> stringData = getStringData(stringVec);
	vector<byte> mineData;
	for(auto& mineDataString : stringData)
	{
		mineData = getMineData(mineDataString);
		mineData.clear();
		count++;
	}
	

	return ret;
}

std::vector<byte> MineField::getMineData(const std::vector<std::string> stringData)
{
	vector<byte> ret;
	
	
	return ret;
}

std::vector<std::vector<std::string>> MineField::getStringData(const std::vector<std::string> stringVec)
{
	vector<vector<string>> ret;
	vector<string> mineDataString;
	int x, y;
	stringstream tmp;
	for (auto& strRow : stringVec)
	{
		x = y = 0;
		tmp = stringstream(strRow);
		tmp >> x;
		tmp >> y;
		
		if(x!=0 && y!=0)
		{
			ret.emplace_back(mineDataString);
			mineDataString.clear();
		}
		mineDataString.emplace_back(strRow);
	}
	return ret;
}
