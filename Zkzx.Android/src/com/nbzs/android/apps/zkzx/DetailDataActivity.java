package com.nbzs.android.apps.zkzx;

import android.app.Activity;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.LayoutInflater;
import android.widget.LinearLayout;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.Map;

/**
 * Created with IntelliJ IDEA.
 * User: Administrator
 * Date: 13-1-10
 * Time: 下午8:54
 * To change this template use File | Settings | File Templates.
 */
public class DetailDataActivity extends Activity {
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.detaildata_layout);

        Bundle extras = getIntent().getExtras();
        if (extras == null) {
            return;
        }
        String exp = extras.getString("exp");
        if (TextUtils.isEmpty(exp))
            return;

        (new LoadDataTask("DSRS_实时监控_车辆作业_作业监控区", exp+"&detail=true"){
            @Override
            protected void onPostExecute(ArrayList<Map<String, String>> mEntries) {
                if (mEntries.size() != 1)
                    return;

                LinearLayout linearView = (LinearLayout)findViewById(R.id.DetailMain);
                linearView.removeAllViews();

                Map<String, String> d = mEntries.get(0);
                Iterator<java.util.Map.Entry<String, String>> iter = d.entrySet().iterator();
                while(iter.hasNext()){
                    java.util.Map.Entry<String, String> kvp = iter.next();
                    if (TextUtils.isEmpty(kvp.getValue()) || kvp.getValue() == "null")
                        continue;

                    LinearLayout v = (LinearLayout) LayoutInflater.from(DetailDataActivity.this).inflate(R.layout.rowtemplate_layout, linearView, false);
                    ((TextView)v.findViewById(R.id.Name)).setText(kvp.getKey());
                    ((TextView)v.findViewById(R.id.Value)).setText(kvp.getValue());
                    linearView.addView(v);
                }
            }
        }).execute(0);
    }
}
