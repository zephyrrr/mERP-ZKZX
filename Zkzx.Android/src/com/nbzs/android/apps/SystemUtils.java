package com.nbzs.android.apps;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.Uri;
import android.os.Environment;
import android.os.PowerManager;
import android.util.Log;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.text.SimpleDateFormat;

/**
 * Created by IntelliJ IDEA.
 * User: Administrator
 * Date: 12-3-14
 * Time: 上午10:25
 * To change this template use File | Settings | File Templates.
 */
public class SystemUtils {
    private static final String TAG = "nbzs.android.SystemUtils";

    public static boolean isOnline(Context ctx) {
        ConnectivityManager cm =
                (ConnectivityManager) ctx.getSystemService(Context.CONNECTIVITY_SERVICE);

        NetworkInfo ni = cm.getActiveNetworkInfo();
        if (ni!=null && ni.isAvailable() && ni.isConnected()) {
            return true;
        } else {
            return false;
        }

    }


    private static PowerManager.WakeLock wakeLock;
    public static void acquireWakeLock(Context ctx, int wakeLockFlag) {
        try {
            PowerManager pm = (PowerManager) ctx.getSystemService(Context.POWER_SERVICE);
            if (pm == null) {
                Log.e(TAG,
                        "Power manager not found!");
                return;
            }
            if (wakeLock == null) {
                wakeLock = pm.newWakeLock(wakeLockFlag,
                        TAG);
                if (wakeLock == null) {
                    Log.e(TAG,
                            "Could not create wake lock (null).");
                    return;
                }
            }
            if (!wakeLock.isHeld()) {
                wakeLock.acquire();
                if (!wakeLock.isHeld()) {
                    Log.e(TAG,
                            "Could not acquire wake lock.");
                }
            }
        } catch (RuntimeException e) {
            SystemUtils.processException(e);
        }
    }

    /**
     * Releases the wake lock if it's currently held.
     */
    public static void releaseWakeLock() {
        if (wakeLock != null && wakeLock.isHeld()) {
            wakeLock.release();
            wakeLock = null;
        }
    }

    public static void install(File file, Context ctx)
    {
        Intent intent = new Intent(Intent.ACTION_VIEW);
        intent.setDataAndType(Uri.fromFile(file), "application/vnd.android.package-archive");
        ctx.startActivity(intent);
    }

    public static File downloadFile(String sUrl)
    {
        try {
            //set the download URL, a url that points to a file on the internet
            //this is the file to be downloaded
            URL url = new URL(sUrl);
            int slashIndex = sUrl.lastIndexOf('/');
            String fileName = sUrl.substring(slashIndex + 1);

            //create the new connection
            HttpURLConnection urlConnection = (HttpURLConnection) url.openConnection();

            //set up some things on the connection
            urlConnection.setRequestMethod("GET");
            // http://stackoverflow.com/questions/9365829/filenotfoundexception-for-httpurlconnection-in-ice-cream-sandwich
            //urlConnection.setDoOutput(true);

            //and connect!
            urlConnection.connect();
            //this will be used in reading the data from the internet
            InputStream inputStream = urlConnection.getInputStream();

            //this is the total size of the file
            int totalSize = urlConnection.getContentLength();
            //variable to store total downloaded bytes
            int downloadedSize = 0;

            //create a buffer...
            byte[] buffer = new byte[1024];
            int bufferLength = 0; //used to store a temporary size of the buffer

            //set the path where we want to save the file
            //in this case, going to save it on the root directory of the
            //sd card.
            //File dir = Environment.getExternalStorageDirectory();
            File dir = Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_DOWNLOADS);
            //create a new file, specifying the path, and the filename
            //which we want to save the file as.
            File file = new File(dir,fileName);
            //this will be used to write the downloaded data into the file we created
            FileOutputStream fileOutput = new FileOutputStream(file);

            //now, read through the input buffer and write the contents to the file
            while ( (bufferLength = inputStream.read(buffer)) > 0 ) {
                //add the data in the buffer to the file in the file output stream (the file on the sd card
                fileOutput.write(buffer, 0, bufferLength);
                //add up the size so we know how much is downloaded
                downloadedSize += bufferLength;
                //this is where you would do something to report the prgress, like this maybe
                //updateProgress(downloadedSize, totalSize);

            }
            //close the output stream when done
            fileOutput.close();
            return file;
//catch some possible errors...
        } catch (MalformedURLException e) {
            SystemUtils.processException(e);
        } catch (IOException e) {
            SystemUtils.processException(e);
        }
        return null;
    }
    
    public static String getExceptionLog()
    {
        String s = sb.toString();
        sb.delete(0, sb.length());
        return s;
    }
    private static String m_newLine = "\r\n";
    private static StringBuffer sb = new StringBuffer();
    public static void processException(Exception ex)
    {
        Log.w(TAG, ex.getMessage(), ex);

        /*java.text.DateFormat dateFormat = new SimpleDateFormat("yyyy/MM/dd HH:mm:ss");
        java.util.Date date = new java.util.Date();
        sb.append(dateFormat.format(date));
        sb.append(m_newLine);
        sb.append(ex.getClass());
        sb.append(m_newLine);
        sb.append(ex.getMessage());
        sb.append(m_newLine);
        sb.append(ex.getStackTrace());
        sb.append(m_newLine);*/
    }

    public static boolean startActivity(Context context, Class<? extends Activity> activityClass) {
        context.startActivity(new Intent(context, activityClass));
        return true;
    }
    public static boolean startActivityForResult(Activity context, Class<? extends Activity> activityClass, int requestCode) {
        context.startActivityForResult(new Intent(context, activityClass), requestCode);
        return true;
    }

    public static int getVersionCode(Context ctx) throws Exception{
        //获取packagemanager的实例
        PackageManager packageManager = ctx.getPackageManager();
        //getPackageName()是你当前类的包名，0代表是获取版本信息
        PackageInfo packInfo = packageManager.getPackageInfo(ctx.getPackageName(), 0);
        return packInfo.versionCode;
    }
}
