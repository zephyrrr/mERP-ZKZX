package com.nbzs.android.apps.zkzx;

import java.util.ArrayList;
import java.util.Map;

/**
 * Created with IntelliJ IDEA.
 * User: Administrator
 * Date: 13-1-10
 * Time: 下午9:15
 * To change this template use File | Settings | File Templates.
 */
public class MainLoadDataTask extends LoadDataTask {
    private final MainDataArrayAdapter mAdapter;
    public MainLoadDataTask(MainDataArrayAdapter adapter) {
        super("DSRS_实时监控_车辆作业_作业监控区", "");
        mAdapter = adapter;
    }

    @Override
    protected void onPostExecute(ArrayList<Map<String, String>> entries) {
        mAdapter.addEntries(entries);
    }
}
