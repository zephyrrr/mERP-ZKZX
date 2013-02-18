package com.nbzs.android.apps.zkzx;

/**
 * Created with IntelliJ IDEA.
 * User: Administrator
 * Date: 13-1-10
 * Time: 下午12:09
 * To change this template use File | Settings | File Templates.
 */
import android.content.Context;
import android.text.TextUtils;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.*;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.Map;

public class MainDataArrayAdapter  extends BaseAdapter {

    private Context mContext;

    private LayoutInflater mLayoutInflater;

    private ArrayList<Map<String, String>> mEntries = new ArrayList<Map<String, String>>();

    public MainDataArrayAdapter(Context context) {
        mContext = context;
        mLayoutInflater = (LayoutInflater) mContext
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
    }

    @Override
    public int getCount() {
        return mEntries.size();
    }

    @Override
    public Object getItem(int position) {
        return mEntries.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        ViewGroup itemView;
        if (convertView == null) {
            itemView = (ViewGroup) mLayoutInflater.inflate(R.layout.maindata_rowlayout, parent, false);

        } else {
            itemView = (ViewGroup) convertView;
        }

        LinearLayout linearView = (LinearLayout)itemView.findViewById(R.id.ItemsList);
        linearView.removeAllViews();

        Map<String, String> d = mEntries.get(position);
        Iterator iter = d.entrySet().iterator();
        int cnt = 0;
        while(iter.hasNext()){
            java.util.Map.Entry<String, String> kvp = (java.util.Map.Entry<String, String>)iter.next();
            if (TextUtils.isEmpty(kvp.getValue()) || kvp.getValue() == "null")
                continue;

            LinearLayout v = (LinearLayout)LayoutInflater.from(mContext).inflate(R.layout.rowtemplate_layout, linearView, false);
            ((TextView)v.findViewById(R.id.Name)).setText(kvp.getKey());
            ((TextView)v.findViewById(R.id.Value)).setText(kvp.getValue());
            linearView.addView(v);

            cnt++;
            if (cnt >= 3)
                break;
        }

        return itemView;
    }

    public void upDateEntries(ArrayList<Map<String, String>> entries) {
        mEntries = entries;
        notifyDataSetChanged();
    }
    public void addEntries(ArrayList<Map<String, String>> entries) {
        if (entries == null)
            return;
        if (mEntries == null)
        {
            mEntries = entries;
        }
        else
        {
            mEntries.addAll(entries);
        }
        notifyDataSetChanged();
    }
}
