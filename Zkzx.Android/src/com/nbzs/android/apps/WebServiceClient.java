package com.nbzs.android.apps;

import android.content.Context;
import android.text.TextUtils;
import android.util.Log;
import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.ResponseHandler;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.BasicResponseHandler;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.params.BasicHttpParams;
import org.apache.http.params.HttpConnectionParams;
import org.apache.http.params.HttpParams;

import java.io.*;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;

/**
 * Created with IntelliJ IDEA.
 * User: Administrator
 * Date: 13-1-10
 * Time: 上午10:40
 * To change this template use File | Settings | File Templates.
 */
public class WebServiceClient {
    public static final String TAG = "nbzs.android.WebServiceClient";

    //private Context m_ctx;
    public WebServiceClient(String serverAddr, String serviceAddr) {
        //m_ctx = context;
        m_serverAddr = serverAddr;
        m_serviceAddr = serviceAddr;
        disableConnectionReuseIfNecessary();
    }
    public void SetServer(String serverAddr)
    {
        m_serverAddr = serverAddr;
    }
    private String m_serverAddr;
    private String m_serviceAddr;
    private final int defaultTimeout = 30000;

    private void disableConnectionReuseIfNecessary() {
        // HTTP connection reuse which was buggy pre-froyo
        if (Integer.parseInt(android.os.Build.VERSION.SDK) < android.os.Build.VERSION_CODES.FROYO) {
            System.setProperty("http.keepAlive", "false");
        }
    }
    private String readStream(InputStream in) throws IOException
    {
        StringBuilder sb = new StringBuilder();
        BufferedReader r = new BufferedReader(new InputStreamReader(in),1000);
        for (String line = r.readLine(); line != null; line =r.readLine()){
            sb.append(line);
        }
        in.close();
        return sb.toString();
    }
    private String PostHttp1(final String address, final String data, Boolean getResult)
    {
        String newUrl = m_serverAddr + m_serviceAddr + address;
        Log.d(TAG, "post web " + newUrl + "," + data.toString());
        HttpURLConnection urlConnection = null;

        try {
            URL url = new URL(newUrl);
            urlConnection = (HttpURLConnection) url.openConnection();
            urlConnection.setRequestMethod("POST");
            urlConnection.setDoInput(true);
            urlConnection.setDoOutput(true);
            urlConnection.setChunkedStreamingMode(0);
            urlConnection.setRequestProperty("Content-Type", "application/json");

            OutputStream out = new BufferedOutputStream(urlConnection.getOutputStream());
            //OutputStreamWriter out = new OutputStreamWriter(urlConnection.getOutputStream());
            out.write(data.getBytes());
            out.flush();

            InputStream in = new BufferedInputStream(urlConnection.getInputStream());
            String result = readStream(in);
            Log.d(TAG, "get web result " + result);
            return result;
        }
        catch (Exception e) {
            SystemUtils.processException(e);
            return null;
        }
        finally {
            if (urlConnection != null)
            {
                urlConnection.disconnect();
            }
        }

    }
    private String GetHttp1(final String address) {
        String newUrl = m_serverAddr + m_serviceAddr + address;
        Log.d(TAG, "get web " + newUrl);

        HttpURLConnection urlConnection = null;
        try {
            URL url = new URL(newUrl);
            urlConnection = (HttpURLConnection) url.openConnection();
            urlConnection.setRequestProperty("Accept-Encoding", null);
            urlConnection.setRequestProperty("User-Agent", null);
            urlConnection.setRequestProperty("Host", null);
            InputStream in = new BufferedInputStream(urlConnection.getInputStream());
            String result = readStream(in);
            Log.d(TAG, "get web result " + result);
            return result;
        }
        catch (Exception e) {
            SystemUtils.processException(e);
            return null;
        }
        finally {
            if (urlConnection != null)
            {
                urlConnection.disconnect();
            }
        }
    }

    public String PostHttp(final String url, final String data, Boolean getResult) {
        String newUrl = m_serverAddr + m_serviceAddr + url;
        Log.d(TAG, "post web " + newUrl + "," + data.toString());

        HttpParams httpParameters = new BasicHttpParams();
// Set the timeout in milliseconds until a connection is established.
        HttpConnectionParams.setConnectionTimeout(httpParameters, defaultTimeout);
// Set the default socket timeout (SO_TIMEOUT)
// in milliseconds which is the timeout for waiting for data.
        HttpConnectionParams.setSoTimeout(httpParameters, defaultTimeout);
        HttpClient httpClient = new DefaultHttpClient(httpParameters);

        // Execute the request
        ResponseHandler<String> handler;
        try {
            StringEntity postData = new StringEntity(data.toString(), "UTF-8");
            postData.setContentType("application/json");
            HttpPost httpPost = new HttpPost(newUrl);
            httpPost.setEntity(postData);
            // no need after setContentType in postData
            //httpPost.setHeader("Accept", "application/json");
            //httpPost.setHeader("content-type", "application/json");
            //httpPost.setHeader("Cache-Control", "no-cache");

            String result = "";
            if (getResult)
            {
                handler = new BasicResponseHandler();
                httpClient.execute(httpPost, handler);
            }
            else
            {
                HttpResponse httpResponse = httpClient.execute(httpPost);
            }
            Log.d(TAG, "post web result " + result);
            return result;
        } catch (ClientProtocolException e) {
            SystemUtils.processException(e);
        } catch (IOException e) {
            SystemUtils.processException(e);
        }
        catch (Exception e) {
            SystemUtils.processException(e);
        }
        return null;
    }

    public String GetHttp(final String url) {
        final String safeUrl;
        /*try
        {
            safeUrl = URLEncoder.encode(url, "UTF-8");
        }
        catch (UnsupportedEncodingException ex)
        {
            Log.d(Constants.TAG, "UnsupportedEncodingException: " + ex.getMessage());
            return null;
        }*/
        HttpParams httpParameters = new BasicHttpParams();
// Set the timeout in milliseconds until a connection is established.
        HttpConnectionParams.setConnectionTimeout(httpParameters, defaultTimeout);
// Set the default socket timeout (SO_TIMEOUT)
// in milliseconds which is the timeout for waiting for data.
        HttpConnectionParams.setSoTimeout(httpParameters, defaultTimeout);
        HttpClient httpClient = new DefaultHttpClient(httpParameters);

        String newUrl = m_serverAddr + m_serviceAddr + url;

        Log.d(TAG, "get web " + newUrl);
        // Prepare a request object
        HttpGet httpget = new HttpGet(newUrl);
        //httpget.setHeader("Cache-Control", "no-cache");

        // Execute the request
        HttpResponse response;
        ResponseHandler<String> handler = new BasicResponseHandler();
        try {
            String result = httpClient.execute(httpget, handler);
            Log.d(TAG, "get web result " + result);
            return result;
        } catch (ClientProtocolException e) {
            SystemUtils.processException(e);
        } catch (IOException e) {
            SystemUtils.processException(e);
        }
        return null;
    }

    /*public boolean CheckService() {
        if (m_serverAddr == null || TextUtils.isEmpty(m_serverAddr))
            return false;
        return SystemUtils.isOnline(m_ctx);
    }*/
}
