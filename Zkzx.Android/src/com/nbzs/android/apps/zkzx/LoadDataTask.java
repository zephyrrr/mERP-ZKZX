package com.nbzs.android.apps.zkzx;

import android.os.AsyncTask;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

/**
 * Created with IntelliJ IDEA.
 * User: Administrator
 * Date: 13-1-10
 * Time: 下午12:26
 * To change this template use File | Settings | File Templates.
 */
public class LoadDataTask extends AsyncTask<Integer, Void, ArrayList<Map<String, String>>> {

    private com.nbzs.android.apps.WebServiceClient m_serviceClient;
    private String m_serviceAddress;
    private String m_exp;
    public LoadDataTask(String serviceAddress, String newExp) {
        m_serviceAddress = serviceAddress;
        m_exp = newExp;
        m_serviceClient = new com.nbzs.android.apps.WebServiceClient("http://17haha8.gicp.net",
                String.format("/Zkzx/Generated/%s.svc", serviceAddress));
    }

    @Override
    protected ArrayList<Map<String, String>> doInBackground(Integer... params) {
        try
        {
            Integer page = params[0];
            String result = m_serviceClient.GetHttp("/?format=json&exp=" + m_exp.toString() + "&order=&first=" + Integer.toString(page * 20) + "&count=20");

            JSONArray jArray = new JSONArray(result);
            ArrayList<Map<String, String>> list = new ArrayList<Map<String, String>>();
            for(int i=0; i<jArray.length(); ++i)
            {
                HashMap<String, String> map = new HashMap<String, String>();
                JSONObject e = jArray.getJSONObject(i);
                Iterator iter = e.keys();
                while(iter.hasNext()){
                    String key = (String)iter.next();
                    map.put(key,  e.getString(key));
                }
                list.add(map);
            }
            return list;
        }
        catch (Exception ex)
        {
            com.nbzs.android.apps.SystemUtils.processException(ex);
            return null;
        }
    }
}
