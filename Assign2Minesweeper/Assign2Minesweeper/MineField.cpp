#include "MineField.h"

using std::vector;
using std::string;
using std::stringstream;

MineField::~MineField()
{}

std::vector<MineField> MineField::getMineFields(const std::vector<std::string>& stringVec)
{
	vector<MineField> ret;
	//vector<vector<byte>> fieldData;
	
	int count = 0;
	std::vector<std::vector<std::string>> stringData = getStringData(stringVec);
	mineFieldData mineData;
	for(auto& mineDataString : stringData)
	{
		mineData = getMineData(mineDataString);
		ret.emplace_back(MineField(mineData,count));
		mineData.clear();
		count++;
	}
	

	return ret;
}

mineFieldData MineField::getMineData(const std::vector<std::string>& stringData)
{
	mineFieldData ret;
	vector<point> minePos;
	for(size_t i = 0;i<stringData.size();i++)
	{
		vector<byte> tmp;
		for(size_t j =0;j<stringData[i].size();j++)
		{
			if(stringData[i][j] == '*')
			{
				tmp.emplace_back(MINE);
				minePos.emplace_back(point(i,j));
			}
			else
			{
				tmp.emplace_back(0);
			}
		}
		ret.emplace_back(tmp);
	}
	for(auto& field: ret)
	{
		updateMineField(field,minePos);
	}
	
	return ret;
}

std::vector<std::vector<std::string>> MineField::getStringData(const std::vector<std::string>& stringVec)
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

void MineField::updateMineField(mineFieldData& fieldData, std::vector<point>& minePos)
{
	
}
