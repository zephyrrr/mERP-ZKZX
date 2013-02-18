package com.nbzs.android.apps.zkzx;

/**
 * Created with IntelliJ IDEA.
 * User: Administrator
 * Date: 13-1-10
 * Time: 上午11:09
 * To change this template use File | Settings | File Templates.
 */
import android.app.ListActivity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.*;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class MainDataListActivity extends ListActivity {
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.maindata_layout);

        MainDataArrayAdapter adapter = new MainDataArrayAdapter(this);

        ListView listView = (ListView)findViewById(android.R.id.list);
        listView.setOnScrollListener(new EndlessScrollListener(adapter));
        listView.setAdapter(adapter);
        listView.setOnItemClickListener(new AdapterView.OnItemClickListener(){
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                Map<String, String> item = (Map<String, String>) adapterView.getAdapter().getItem(i);
                Intent intent = new Intent(getBaseContext(), DetailDataActivity.class);
                intent.putExtra("exp", "作业号%20=%20" + item.get("作业号"));
                startActivity(intent);
            }
        });

        MainLoadDataTask loadFeedData = new MainLoadDataTask(adapter);
        loadFeedData.execute(0);
    }

    @Override
    protected void onListItemClick(ListView l, View v, int position, long id) {

    }
}
